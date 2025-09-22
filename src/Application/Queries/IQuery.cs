namespace XIVSubmarinesRewrite.Application.Queries;

/// <summary>Marker interface for query messages handled via the mediator.</summary>
/// <typeparam name="TResponse">Response type produced by the query handler.</typeparam>
public interface IQuery<out TResponse>
{
}
