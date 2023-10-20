namespace RemoteService.Models;

[Flags]
public enum State {
    New     = 0x00000,
    Ready   = 0x00001,
    Private = 0x00100,
    Hidden  = 0x10000,
}
