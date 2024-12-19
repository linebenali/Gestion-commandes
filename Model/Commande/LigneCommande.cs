// 
public class LigneCommande
{
    public int CommandeId { get; set; } // Identifiant de l'article
    public int ArticleId { get; set; } // Identifiant de l'article
    public string NomArticle { get; set; } // Nom de l'article (facultatif)
    public int Quantite { get; set; } // Quantité commandée
    public decimal PrixUnitaire { get; set; } // Prix unitaire de l'article
    public decimal Total => Quantite * PrixUnitaire; // Total pour cette ligne
}
