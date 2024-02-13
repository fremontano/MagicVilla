using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        //inyectar servicio de logger creando un constructor
        //VARIABLES PRIVADAS van con guion bajo _varPrivada
        private readonly ILogger<VillaController> _logger;
        //inyectar para guardar en la  base de datos desde las rutas de mi api
        private readonly ApplicationDbContext _db;
        //para recorrer con el map
        private readonly IMapper _mapper;






        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db, IMapper mapper)
        {
            //se inicializa la variable
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }






        //retornara todos los objectos de la lista 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>>GetVillas()
        {

            _logger.LogInformation("Obteniedo las Villas");
            //creamos una lista para recoger los datos
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            //retornarlo con map
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
        }


        //retornara un solo objecto de la lista por  medio del parametro id
        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVillas(int id)
        {

            //validaciones
            if (id == 0)
            {
                _logger.LogError("Error al traer la Villa con el Id " + id);
                return BadRequest();
            }

            //no encuentra ningun registro
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
             var villa =await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);


            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        //crear nuevas villas
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createVillaDto)
        {



            //campo requerido validar campo vacio si falla el modelo
            if (!ModelState.IsValid)
            {
                _logger.LogError("debes ingresar valores en el campo");
                return BadRequest(ModelState);
            }

            //validar nombre repetidos
            if (await _db.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == createVillaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            //recibiendo informacion
            if (createVillaDto == null)
            {
                return BadRequest(createVillaDto);
            }

            //remplazar el codigo por una sola linea mapeando con mapper
            Villa modelo = _mapper.Map<Villa>(createVillaDto);
            

            //agregar a la base de datos
            await _db.AddAsync(modelo);
            //para que los cambios se vean reflejados en la base de datos
            await _db.SaveChangesAsync();

           
            //retornar villaDto
            return CreatedAtRoute("GetVilla", new { id = modelo.Id}, createVillaDto);
        }


        //eliminar un objecto de mi lista de villa
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> DeleteVilla(int id)
        {

            // si no envia el id de manera apropiado
            if (id == 0)
            {
                return BadRequest();
            }
            //no coincide el id que tengamos en la lista
            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            //eliminar recien el objecto con el id correspondiente
            _logger.LogInformation("Se elimino un registro de la lista");
            //le envio de mi variable villa
            _db.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }


        
        //actualizar objecto de la lista
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateVillaDto)
        {
            if (updateVillaDto == null || id != updateVillaDto.Id)
            {
                return BadRequest();
            }
         


            //mapear
            Villa modelo = _mapper.Map<Villa>(updateVillaDto);

           
             _db.Update(modelo);
            await _db.SaveChangesAsync();
            

            //para no retornar mis valores
            return NoContent();
        }



        //actualiza de mi objecto una sola propiedad
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task< IActionResult >UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> pacthDto)
        {
            if (pacthDto == null || id == 0)
            {
                return BadRequest();
            }
            //capturar el registro antes de grabarlo
            var villa =await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            // mappeador
            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

           

            if(villa == null) return BadRequest();

            pacthDto.ApplyTo(villaDto, ModelState);
            //para no retornar mis valores
            //si no es valido con el signo de admiracion(!) lo negamos
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }


            //mapeo
            Villa modelo = _mapper.Map<Villa>(villaDto);

            

            _db.Villas.Update(modelo);
            await _db.SaveChangesAsync();



            return NoContent();
        }

    }
}
