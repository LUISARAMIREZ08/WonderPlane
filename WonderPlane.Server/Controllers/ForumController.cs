using WonderPlane.Shared;
using Microsoft.AspNetCore.Mvc;
using WonderPlane.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WonderPlane.Server.Controllers
{
    [Route("api/forum")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ForumController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las preguntas
        [HttpGet("questions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetQuestions()
        {
            var questions = await _context.Questions
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Content = q.Content,
                    Date = q.Date,
                    UserId = q.UserId,
                    Theme = q.Theme,
                    StateQuestion = (QuestionDto.StateDto)q.StateQuestion
                })
                .ToListAsync();

            return Ok(questions);
        }

        // Crear una nueva pregunta
        [HttpPost("add-question")]
        public async Task<ActionResult<string>> CreateQuestion([FromBody] QuestionDto questionDto)
        {
            var question = new Question
            {
                Content = questionDto.Content,
                Date = questionDto.Date,
                UserId = questionDto.UserId,
                Theme = questionDto.Theme,
                StateQuestion = (Question.State)questionDto.StateQuestion
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            questionDto.Id = question.Id;
            return Ok("Pregunta creada con éxito");
        }

        // Obtener una pregunta por ID
        [HttpGet("questions/{id}")]
        public async Task<ActionResult<QuestionDto>> GetQuestionById(int id)
        {
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
                return NotFound(new { Message = "Question not found" });

            var questionDto = new QuestionDto
            {
                Id = question.Id,
                Content = question.Content,
                Date = question.Date,
                UserId = question.UserId,
                Theme = question.Theme,
                StateQuestion = (QuestionDto.StateDto)question.StateQuestion
            };

            return Ok(questionDto);
        }

        // Crear una nueva respuesta
        [HttpPost("add-answer")]
        public async Task<ActionResult<string>> CreateResponse([FromBody] ResponseDto responseDto)
        {
            // Verificar si la pregunta asociada existe
            var question = await _context.Questions.FindAsync(responseDto.QuestionId);
            if (question == null)
                return NotFound(new { Message = "Question not found" });

            // Crear la nueva respuesta
            var response = new Response
            {
                Content = responseDto.Content,
                Date = responseDto.Date,
                QuestionId = responseDto.QuestionId,
                AdminId = responseDto.AdminId
            };

            _context.Responses.Add(response);

            // Cambiar el estado de la pregunta a "Respondido" si no está ya en ese estado
            if (question.StateQuestion != Question.State.Respondida)
            {
                question.StateQuestion = Question.State.Respondida;
                _context.Questions.Update(question);
            }

            // Obtener el correo del usuario para enviarle una notificación 
            var user = await _context.Users.FindAsync(question.UserId);
            var userEmail = user?.Email;


            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            responseDto.Id = response.Id;
            return Ok(userEmail);

        }


        // Obtener todas las respuestas asociadas a una pregunta por su ID
        [HttpGet("questions/{id}/responses")]
        public async Task<ActionResult<IEnumerable<ResponseDto>>> GetResponsesByQuestionId(int id)
        {
            var question = await _context.Questions.Include(q => q.Responses)
                                                   .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
                return NotFound(new { Message = "Question not found" });

            var responsesDto = question.Responses.Select(r => new ResponseDto
            {
                Id = r.Id,
                Content = r.Content,
                Date = r.Date,
                QuestionId = r.QuestionId,
                AdminId = r.AdminId
            }).ToList();

            return Ok(responsesDto);
        }


    }

}
