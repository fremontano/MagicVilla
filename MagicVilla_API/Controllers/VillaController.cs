using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {

        //retornara todos los objectos de la lista 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villaList);
        }


        //reotornara un solo objecto de la lista por  medio del parametro id
        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVillas(int id)
        {

            //validaciones
            if (id == 0)
            {
                return BadRequest();
            }

            //no encuentra ningun registro
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        //crear nuevas villas
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
        {



            //campo requerido validar campo vacio si falla el modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //validar nombre repetidos
            if (VillaStore.villaList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe!");
                return BadRequest(ModelState);
            }

            //recibiendo informacion
            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }
            if (villaDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //capturar el state del registro antes que se actualize
            villaDto.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //agregar
            VillaStore.villaList.Add(villaDto);
            //retornar villaDto
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id });
        }


        //eliminar un objecto de mi lista de villa
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteVilla(int id)
        {

            // si no envia el id de manera apropiado
            if (id == 0)
            {
                return BadRequest();
            }
            //no coincide el id que tengamos en la lista
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            //eliminar recien el objecto con el id correspondiente
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }


        
        //actualizar objecto de la lista
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            //capturar el registro antes de grabarlo
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            villa.Nombre = villaDto.Nombre;
            villa.Ocupantes = villaDto.Ocupantes;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;
            villa.MetrosCuadrados = villaDto.MetrosCuadrados;

            //para no retornar mis valores
            return NoContent();
        }



        //actualiza de mi objecto una sola propiedad
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> pacthDto)
        {
            if (pacthDto == null || id == 0)
            {
                return BadRequest();
            }
            //capturar el registro antes de grabarlo

            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            pacthDto.ApplyTo(villa, ModelState);
            //para no retornar mis valores
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            return NoContent();
        }

    }
}
