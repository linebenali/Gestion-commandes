using MySql.Data.MySqlClient;
using System;

public class UtilisateurDAO
{
    private string connectionString = "Server=localhost;Database=gestion_commande;Uid=root;Pwd=;";

    // Méthode pour vérifier les identifiants de l'utilisateur
    public Utilisateur Authentifier(string nomUtilisateur, string motDePasse)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Utilisateur WHERE NomUtilisateur = @NomUtilisateur AND MotDePasse = @MotDePasse";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NomUtilisateur", nomUtilisateur);
                    command.Parameters.AddWithValue("@MotDePasse", motDePasse);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Utilisateur
                            {
                                Id = reader.GetInt32("Id"),
                                NomUtilisateur = reader.GetString("NomUtilisateur"),
                                MotDePasse = reader.GetString("MotDePasse"),
                                Role = reader.GetString("Role")
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'authentification : {ex.Message}");
        }
        return null; // Si l'utilisateur n'est pas trouvé
    }
}
