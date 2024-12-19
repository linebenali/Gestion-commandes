public class Commande
{
    public int Id { get; set; } // Identifiant unique
    public int ClientId { get; set; } // Référence au client
    public List<LigneCommande> LignesCommande { get; set; } // Liste des articles dans la commande
    public DateTime DateCommande { get; set; } // Date de la commande
    public string Statut { get; set; } // Statut de la commande (ex. : "En cours", "Livrée")
}
