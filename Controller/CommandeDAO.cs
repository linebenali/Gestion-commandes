using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class CommandeDAO
{
    private string connectionString = "Server=localhost;Database=gestion_commande;Uid=root;Pwd=;";

    // Ajouter une commande
    public bool AjouterCommande(Commande commande)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    // Insérer la commande
                    string commandeQuery = "INSERT INTO commandes (ClientId, DateCommande, Statut) VALUES (@ClientId, @DateCommande, @Statut)";
                    using (var commandeCmd = new MySqlCommand(commandeQuery, connection, transaction))
                    {
                        commandeCmd.Parameters.AddWithValue("@ClientId", commande.ClientId);
                        commandeCmd.Parameters.AddWithValue("@DateCommande", commande.DateCommande);
                        commandeCmd.Parameters.AddWithValue("@Statut", commande.Statut);
                        commandeCmd.ExecuteNonQuery();
                        commande.Id = (int)commandeCmd.LastInsertedId;
                    }

                    // Insérer les lignes de commande
                    foreach (var ligne in commande.LignesCommande)
                    {
                        string ligneQuery = "INSERT INTO lignecommande (CommandeId, ArticleId, Quantite, PrixUnitaire) VALUES (@CommandeId, @ArticleId, @Quantite, @PrixUnitaire)";
                        using (var ligneCmd = new MySqlCommand(ligneQuery, connection, transaction))
                        {
                            ligneCmd.Parameters.AddWithValue("@CommandeId", commande.Id);
                            ligneCmd.Parameters.AddWithValue("@ArticleId", ligne.ArticleId);
                            ligneCmd.Parameters.AddWithValue("@Quantite", ligne.Quantite);
                            ligneCmd.Parameters.AddWithValue("@PrixUnitaire", ligne.PrixUnitaire);
                            ligneCmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'ajout de la commande : {ex.Message}");
            return false;
        }
    }

    // Récupérer toutes les commandes
    public List<Commande> RecupererToutesLesCommandes()
    {
        List<Commande> commandes = new List<Commande>();
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Récupérer les commandes
                string commandeQuery = "SELECT * FROM Commandes";
                using (var commandeCmd = new MySqlCommand(commandeQuery, connection))
                {
                    using (var reader = commandeCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            commandes.Add(new Commande
                            {
                                Id = reader.GetInt32("Id"),
                                ClientId = reader.GetInt32("ClientId"),
                                DateCommande = reader.GetDateTime("DateCommande"),
                                Statut = reader.GetString("Statut"),
                                LignesCommande = new List<LigneCommande>() // À remplir plus tard
                            });
                        }
                    }
                }

                // Récupérer les lignes de commande
                foreach (var commande in commandes)
                {
                    string ligneQuery = "SELECT * FROM LigneCommande WHERE CommandeId = @CommandeId";
                    using (var ligneCmd = new MySqlCommand(ligneQuery, connection))
                    {
                        ligneCmd.Parameters.AddWithValue("@CommandeId", commande.Id);
                        using (var reader = ligneCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                commande.LignesCommande.Add(new LigneCommande
                                {
                                    ArticleId = reader.GetInt32("ArticleId"),
                                    Quantite = reader.GetInt32("Quantite"),
                                    PrixUnitaire = reader.GetDecimal("PrixUnitaire")
                                });
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des commandes : {ex.Message}");
        }
        return commandes;
    }

public bool SupprimerCommande(int commandeId)
{
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                // Supprimer les lignes de commande associées
                string deleteLignesQuery = "DELETE FROM LigneCommande WHERE CommandeId = @CommandeId";
                using (var ligneCmd = new MySqlCommand(deleteLignesQuery, connection, transaction))
                {
                    ligneCmd.Parameters.AddWithValue("@CommandeId", commandeId);
                    ligneCmd.ExecuteNonQuery();
                }

                // Supprimer la commande
                string deleteCommandeQuery = "DELETE FROM Commandes WHERE Id = @CommandeId";
                using (var commandeCmd = new MySqlCommand(deleteCommandeQuery, connection, transaction))
                {
                    commandeCmd.Parameters.AddWithValue("@CommandeId", commandeId);
                    commandeCmd.ExecuteNonQuery();
                }

                // Commit de la transaction
                transaction.Commit();
            }
        }
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de la suppression de la commande : {ex.Message}");
        return false;
    }
}
// Modifier le statut d'une commande
 public bool ModifierStatutCommande(int commandeId, string nouveauStatut)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Commandes SET Statut = @Statut WHERE Id = @CommandeId";
                using (var cmd = new MySqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Statut", nouveauStatut);
                    cmd.Parameters.AddWithValue("@CommandeId", commandeId);
                    cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la modification du statut de la commande : {ex.Message}");
            return false;
        }
    }
public bool AjouterLigneCommande(LigneCommande ligne, int commandeId)
{
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "INSERT INTO LigneCommande (CommandeId, ArticleId, NomArticle, Quantite, PrixUnitaire, Total) " +
                           "VALUES (@CommandeId, @ArticleId, @NomArticle, @Quantite, @PrixUnitaire, @Total)";

            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@CommandeId", commandeId);
                cmd.Parameters.AddWithValue("@ArticleId", ligne.ArticleId);
                cmd.Parameters.AddWithValue("@NomArticle", ligne.NomArticle);
                cmd.Parameters.AddWithValue("@Quantite", ligne.Quantite);
                cmd.Parameters.AddWithValue("@PrixUnitaire", ligne.PrixUnitaire);
                cmd.Parameters.AddWithValue("@Total", ligne.Quantite * ligne.PrixUnitaire);

                cmd.ExecuteNonQuery();
            }
        }
        return true;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'ajout de la ligne de commande : {ex.Message}");
        return false;
    }
}



}



 


