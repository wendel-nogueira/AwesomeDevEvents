using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter todos os eventos
        /// </summary>
        /// <returns>Coleção de eventos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(devEvent => !devEvent.IsDeleted).ToList();

            return Ok(devEvents);
        }

        /// <summary>
        /// Obter um evento
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <returns>Dados do evento</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _context.DevEvents
                .Include(devEvent => devEvent.Speakers)
                .SingleOrDefault(devEvent => devEvent.Id == id && !devEvent.IsDeleted);

            if (devEvent == null)
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        /// <summary>
        /// Cadastrar um novo evento
        /// </summary>
        /// <remarks>
        ///     { "title": "string", "description": "string", "startDate": "2023-09-04T17:07:06.953Z", "endDate": "2023-09-04T17:07:06.953Z" }
        /// </remarks>
        /// <param name="devEvent">Dados do evento</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }

        /// <summary>
        /// Atualizar um evento
        /// </summary>
        /// <remarks>
        ///     { "title": "string", "description": "string", "startDate": "2023-09-04T17:07:06.953Z", "endDate": "2023-09-04T17:07:06.953Z" }
        /// </remarks>
        /// <param name="id">Identificador do evento</param>
        /// <param name="devEventInput">Dados do evento</param>
        /// <returns></returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, [FromBody] DevEvent devEventInput)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(
                devEventInput.Title,
                devEventInput.Description,
                devEventInput.StartDate,
                devEventInput.EndDate
            );

            _context.DevEvents.Update(devEvent);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Remover um evento
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <returns></returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(devEvent => devEvent.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();

            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Cadastrar palestrante
        /// </summary>
        /// <remarks>
        /// { "name": "string", "talkTitle": "string", "talkDescription": "string", "linkedinProfile": "string", "devEventId": "3fa85f64-5717-4562-b3fc-2c963f66afa6" }
        /// </remarks>
        /// <param name="id">Identificador do evento</param>
        /// <param name="devEventSpeaker">Dados do palestrante</param>
        /// <returns></returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost("{id}/speakers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostSpeaker(Guid id, [FromBody] DevEventSpeaker devEventSpeaker)
        {
            devEventSpeaker.DevEventId = id;

            var devEvent = _context.DevEvents.Any(devEvent => devEvent.Id == id);

            if (!devEvent)
            {
                return NotFound();
            }

            _context.DevEventSpeakers.Add(devEventSpeaker);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
