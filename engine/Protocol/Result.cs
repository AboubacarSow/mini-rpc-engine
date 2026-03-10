namespace engine.Protocol;

public record Result
{
    object Message{get;set;}
    string Error {get;set;}
}