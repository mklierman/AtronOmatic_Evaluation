namespace AtronOmatic_Evaluation.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
