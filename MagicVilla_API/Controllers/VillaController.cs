using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
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
        private readonly IVillaRepositorio _villaRepositorio; //remplazamos el dbContext por la interfaz villa repositorio
        //para recorrer con el map
        private readonly IMapper _mapper;






        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villaRepositorio, IMapper mapper)
        {
            //se inicializa la variable
            _logger = logger;
            _villaRepositorio = villaRepositorio;
            _mapper = mapper;
        }






        //retornara todos los objectos de la lista 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>>GetVillas()
        {

            _logger.LogInformation("Obteniedo las Villas");
            //creamos una lista para recoger los datos
            IEnumerable<Villa> villaList = await _villaRepositorio.ObtenerTodos();
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
             var villa =await _villaRepositorio.Obtener(v => v.Id == id);


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
            if (await _villaRepositorio.Obtener(v => v.Nombre.ToLower() == createVillaDto.Nombre.ToLower()) != null)
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
            await _villaRepositorio.Crear(modelo);
          

           
            //retornar villaDto
            return CreatedAtRoute("GetVilla", new { id = modelo.Id}, createVillaDto);
        }


        //eliminar un objecto de mi lista de villa
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteVilla(int id)
        {

            // si no envía el id de manera apropiada
            if (id == 0)
            {
                return BadRequest();
            }
            // busca la villa por ID
            var villa = await _villaRepositorio.Obtener(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            // elimina la villa del repositorio
            _logger.LogInformation($"Se eliminó la villa con ID: {id}");

            //le envio de mi variable villa
            _villaRepositorio.Remover(villa);
            return NoContent();

        }




        //actualizar objecto de la lista
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null || id!= updateDto.Id)
            {
                return BadRequest();
            }

            // Mapear el DTO de actualización a la entidad de dominio (Villa)
            Villa modelo = _mapper.Map<Villa>(updateDto);

            // Actualizar la villa en el repositorio
            await _villaRepositorio.Actualizar(modelo);
            // Retornar un código de estado 204 No Content para indicar que la operación se realizó con éxito
            return NoContent();
        }




        //actualiza de mi objecto una sola propiedad
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public async Task<IActionResult> UpdateVilla(int id, JsonPatchDocument<VillaUpdateDto> pacthDto)
        {
            if (pacthDto == null || id == 0)
            {
                return BadRequest();
            }
            //capturar el registro antes de grabarlo
            var villa = await _villaRepositorio.Obtener(v => v.Id == id, tracked: false);

            if (villa == null)
            {
                return BadRequest();
            }

            // mapeo de la villa a un DTO
            var villaDto = _mapper.Map<VillaUpdateDto>(villa);

            // aplicar los cambios parciales del JsonPatchDocument al DTO
            pacthDto.ApplyTo(villaDto, ModelState);

            // validar el modelo después de aplicar los cambios parciales
            if (!TryValidateModel(villaDto))
            {
                return BadRequest(ModelState);
            }

            // mapeo del DTO actualizado de vuelta a la villa original
            var modelo =  _mapper.Map(villaDto, villa);

            // actualizar la villa en el repositorio
            _villaRepositorio.Actualizar(modelo);
             

            return NoContent();
        }



    }
}
