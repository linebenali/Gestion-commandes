using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
public class ClientDAO
{
    private string connectionString = "Server=localhost;Database=gestion_commande;Uid=root;Pwd=;";

    // Méthode pour ajouter un client
    public bool AjouterClient(Client client)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Client (Nom, Adresse, Telephone) VALUES (@Nom, @Adresse, @Telephone)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nom", client.Nom);
                    command.Parameters.AddWithValue("@Adresse", client.Adresse);
                    command.Parameters.AddWithValue("@Telephone", client.Telephone);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'ajout du client : {ex.Message}");
            return false;
        }
    }

    // Méthode pour récupérer tous les Client
    public List<Client> RecupererTousLesClient()
    {
        List<Client> Client = new List<Client>();
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Client";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Client.Add(new Client
                            {
                                Id = reader.GetInt32("Id"),
                                Nom = reader.GetString("Nom"),
                                Adresse = reader.GetString("Adresse"),
                                Telephone = reader.GetString("Telephone")
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des Client : {ex.Message}");
        }
        return Client;
    }


    // Méthode pour modifier un client existant
public bool ModifierClient(Client client)
{
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE client SET Nom = @Nom, Adresse = @Adresse, Telephone = @Telephone WHERE Id = @Id";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", client.Id);
                command.Parameters.AddWithValue("@Nom", client.Nom);
                command.Parameters.AddWithValue("@Adresse", client.Adresse);
                command.Parameters.AddWithValue("@Telephone", client.Telephone);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de la modification du client : {ex.Message}");
        return false;
    }
}
// Méthode pour récupérer un client par son Id
    public Client RecupererClientParId(int id)
    {
        Client client = null;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Client WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = new Client
                            {
                                Id = reader.GetInt32("Id"),
                                Nom = reader.GetString("Nom"),
                                Adresse = reader.GetString("Adresse"),
                                Telephone = reader.GetString("Telephone")
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération du client : {ex.Message}");
        }
        return client;
    }

// Méthode pour supprimer un client par son Id
public bool SupprimerClient(int id)
{
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM Client WHERE Id = @Id";
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de la suppression du client : {ex.Message}");
        return false;
    }
}


}

