public class Utilisateur
{
    public int Id { get; set; } // Identifiant unique
    public string NomUtilisateur { get; set; } // Nom d'utilisateur
    public string MotDePasse { get; set; } // Mot de passe
    public string Role { get; set; } // Rôle (ex. : "Administrateur", "Commercial")
}
