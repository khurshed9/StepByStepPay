namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductService service) : BaseController
{
    [HttpGet] public async Task<IActionResult> Get([FromQuery] ProductFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();

    [HttpGet("{id:int}")] public async Task<IActionResult> Get([FromRoute] int id)
        => (await service.GetByIdAsync(id)).ToActionResult();

    [HttpPost] public async Task<IActionResult> Create([FromForm] ProductCreateInfo entity)
        => (await service.CreateAsync(entity)).ToActionResult();

    [HttpPut("{id:int}")] public async Task<IActionResult> Update([FromRoute] int id, [FromForm] ProductUpdateInfo entity)
        => (await service.UpdateAsync(id, entity)).ToActionResult();

    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete([FromRoute] int id)
        => (await service.DeleteAsync(id)).ToActionResult();
}