using RemoteService.Controllers.Common;
using RemoteService.Controllers.Tenants.Models;
using RemoteService.Handlers.Tenants;

namespace RemoteService.Controllers.Tenants;

[Authorize]
[ApiController]
[Route("api/v{version:apiVersion}/tenants", Order = 1)]
[ApiExplorerSettings(GroupName = "Tenants")]
[Produces("application/json")]
public class TenantController : ControllerBase {
    private readonly ITenantHandler _handler;
    private readonly ILogger<TenantController> _logger;

    public TenantController(ITenantHandler handler, ILogger<TenantController> logger) {
        _handler = handler;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all tenants",
                      Description = "Retrieves a collection of tenants.",
                      OperationId = "GetTenantById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantRowResponse[]))]
    public async Task<IActionResult> GetMany(CancellationToken ct = default) {
        _logger.LogDebug("Getting all tenants requested.");
        var result = await _handler.GetManyAsync(ct);
        var response = result.Value!.ToResponse();
        _logger.LogDebug("{count} tenants retrieved successfully.", response.Length);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a tenant",
                      Description = "Retrieves a tenant by its ID.",
                      OperationId = "GetTenantById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromRoute]
        [SwaggerParameter("The id of the tenant.", Required = true)]
        string id,
        CancellationToken ct = default) {
        _logger.LogDebug("Getting tenant '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var result = await _handler.GetByIdAsync(uuid, ct);
        if (result.Value is null) {
            _logger.LogDebug("Fail to retrieve tenant '{id}' (not found).", id);
            return NotFound();
        }

        var response = result.Value!.ToResponse();
        _logger.LogDebug("Tenant '{id}' retrieved successfully.", id);
        return Ok(response);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a tenant",
                      Description = "Creates a new tenant using the provided request data.",
                      OperationId = "CreateTenant")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TenantResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    public async Task<IActionResult> Create(
        [FromBody]
        [SwaggerParameter("New tenant data.", Required = true)]
        TenantRequest request,
        CancellationToken ct = default) {
        _logger.LogDebug("Create tenant requested.");
        var model = request.ToDomain();
        var result = await _handler.AddAsync(model, ct);
        if (result.IsInvalid) {
            _logger.LogDebug("Fail to create tenant (bad request).");
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.HasConflict) {
            _logger.LogDebug("Fail to create tenant (conflict).");
            return Conflict("A tenant with same name already exists.");
        }

        var response = result.Value!.ToResponse();
        _logger.LogDebug("Tenant '{id}' created successfully.", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update a tenant",
                      Description = "Updates an existing tenant with the given ID using the provided request data.",
                      OperationId = "UpdateTenant")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute]
        [SwaggerParameter("The id of the tenant.", Required = true)]
        string id,
        [FromBody]
        [SwaggerParameter("Updated tenant data.", Required = true)]
        TenantRequest request,
        CancellationToken ct = default) {
        _logger.LogDebug("Update tenant '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var model = request.ToDomain(uuid);
        var result = await _handler.UpdateAsync(model, ct);
        if (result.IsInvalid) {
            _logger.LogDebug("Fail to update tenant '{id}' (bad request).", id);
            return BadRequest(result.Errors.UpdateModelState(ModelState));
        }

        if (result.WasNotFound) {
            _logger.LogDebug("Fail to update tenant '{id}' (not found).", id);
            return NotFound();
        }

        var response = result.Value!.ToResponse();
        _logger.LogDebug("Tenant '{id}' updated successfully.", id);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove a tenant",
                      Description = "Removes an existing tenant with the given ID.",
                      OperationId = "RemoveTenant")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remove(
        [FromRoute]
        [SwaggerParameter("The id of the tenant.", Required = true)]
        string id,
        CancellationToken ct = default) {
        _logger.LogDebug("Remove tenant '{id}' requested.", id);
        if (!Base64Guid.TryParse(id, out var uuid)) {
            ModelState.AddModelError("id", "Not a valid base64 uuid.");
            return BadRequest(ModelState);
        }

        var result = await _handler.RemoveAsync(uuid, ct);
        if (result.WasNotFound) {
            _logger.LogDebug("Fail to remove tenant '{id}' (not found).", id);
            return NotFound();
        }

        _logger.LogDebug("Tenant '{id}' removed successfully.", id);
        return Ok();
    }
}
