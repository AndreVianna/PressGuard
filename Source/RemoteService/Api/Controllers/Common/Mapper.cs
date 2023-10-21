namespace RemoteService.Controllers.Common;

internal static class Mapper {
    public static ModelStateDictionary UpdateModelState(this ICollection<ValidationError> validationErrors, ModelStateDictionary modelState) {
        foreach (var error in validationErrors)
            modelState.AddModelError(error.Arguments[0]!.ToString()!, error.Message);
        return modelState;
    }
}