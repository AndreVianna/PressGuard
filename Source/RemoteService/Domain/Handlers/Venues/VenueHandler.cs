using RemoteService.Repositories.Venues;

namespace RemoteService.Handlers.Venues;

public class VenueHandler
    : CrudHandler<Venue, VenueRow, IVenueRepository>,
      IVenueHandler {
    public VenueHandler(IVenueRepository repository)
        : base(repository) {
    }
}
