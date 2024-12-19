using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

public class ArticleDAO
{
    private string connectionString = "Server=localhost;Database=gestion_commande;Uid=root;Pwd=;";

    // Méthode pour ajouter un nouvel article
    public bool AjouterArticle(Article article)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Articles (Nom, Description, Categorie, Prix) VALUES (@Nom, @Description, @Categorie, @Prix)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nom", article.Nom);
                    command.Parameters.AddWithValue("@Description", article.Description);
                    command.Parameters.AddWithValue("@Categorie", article.Categorie);
                    command.Parameters.AddWithValue("@Prix", article.Prix);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de l'ajout de l'article : {ex.Message}");
            return false;
        }
    }

    // Méthode pour modifier un article existant
    public bool ModifierArticle(Article article)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Articles SET Nom = @Nom, Description = @Description, Categorie = @Categorie, Prix = @Prix WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", article.Id);
                    command.Parameters.AddWithValue("@Nom", article.Nom);
                    command.Parameters.AddWithValue("@Description", article.Description);
                    command.Parameters.AddWithValue("@Categorie", article.Categorie);
                    command.Parameters.AddWithValue("@Prix", article.Prix);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la modification de l'article : {ex.Message}");
            return false;
        }
    }

    // Méthode pour supprimer un article
    public bool SupprimerArticle(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Articles WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la suppression de l'article : {ex.Message}");
            return false;
        }
    }

    // Méthode pour récupérer un article par son ID
    public Article RecupererArticleParId(int id)
    {
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Articles WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Article
                            {
                                Id = reader.GetInt32("Id"),
                                Nom = reader.GetString("Nom"),
                                Description = reader.GetString("Description"),
                                Categorie = reader.GetString("Categorie"),
                                Prix = reader.GetDecimal("Prix")
                            };
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération de l'article : {ex.Message}");
        }
        return null;
    }

    // Méthode pour récupérer tous les articles
    public List<Article> RecupererTousLesArticles()
    {
        List<Article> articles = new List<Article>();
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Articles";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            articles.Add(new Article
                            {
                                Id = reader.GetInt32("Id"),
                                Nom = reader.GetString("Nom"),
                                Description = reader.GetString("Description"),
                                Categorie = reader.GetString("Categorie"),
                                Prix = reader.GetDecimal("Prix")
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la récupération des articles : {ex.Message}");
        }
        return articles;
    }
}
