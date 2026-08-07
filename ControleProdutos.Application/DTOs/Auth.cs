// =============================================================================
// SenacGames.Application - DTOs de Autenticação
// =============================================================================
// DTOs usados nos endpoints de autenticação (Login, Register).
// =============================================================================

namespace ControleProdutos.Application.DTOs
{
    /// <summary>
    /// DTO para login de usuário.
    /// </summary>
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para registro de novo usuário.
    /// </summary>
    public class RegisterDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para retorno de dados do usuário autenticado.
    /// </summary>
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
