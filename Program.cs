namespace projet;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
       // Application.Run(new Form1());
       //Application.Run(new ArticlesForm());
   // Afficher la fenêtre de connexion
        LoginForm loginForm = new LoginForm();
    if (loginForm.ShowDialog() == DialogResult.OK)
        {
            Utilisateur utilisateurConnecte = loginForm.UtilisateurConnecte;
            MainForm mainForm = new MainForm(utilisateurConnecte);
            Application.Run(mainForm);
        }
       else
        {
            MessageBox.Show("Connexion échouée. Fermeture de l'application.");
        }
    }    
}