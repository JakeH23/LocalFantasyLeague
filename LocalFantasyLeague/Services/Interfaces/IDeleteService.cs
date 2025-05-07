namespace LocalFantasyLeague.Services.Interfaces;

public interface IDeleteService
{
    Task DeleteAllCorrespondingMatchData(int matchId);
}