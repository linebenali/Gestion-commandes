public class MainForm : Form
{
    private Utilisateur utilisateurConnecte;

    public MainForm(Utilisateur utilisateur)
    {
        utilisateurConnecte = utilisateur;
       
        // En fonction du rôle de l'utilisateur, afficher les fonctionnalités
        if (utilisateurConnecte.Role == "Administrateur")
        {
            // Ouvrir les fonctionnalités réservées aux administrateurs
            OuvrirFenetreAdministrateur();
        }
        else if (utilisateurConnecte.Role == "Commercial")
        {
            // Ouvrir les fonctionnalités réservées aux commerciaux
            OuvrirFenetreCommercial();
        }
    }

    private void OuvrirFenetreAdministrateur()
    {        Interface1 AdminTasks = new Interface1();
             AdminTasks.ShowDialog();

    }

    private void OuvrirFenetreCommercial()
    {
         InterfaceCommerciale CommerTasks = new InterfaceCommerciale();
             CommerTasks.ShowDialog();

    }
}
