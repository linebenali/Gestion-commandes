using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class CommandeForm : Form
{
    private ComboBox cboClients, cboArticles;
    private DataGridView dgvArticles;
    private Button btnAjouterArticle, btnValiderCommande;
    private TextBox txtQuantite;
    private Commande commande;
    private ClientDAO clientDAO;
    private ArticleDAO articleDAO;

    public CommandeForm()
    {
        clientDAO = new ClientDAO();
        articleDAO = new ArticleDAO();
        commande = new Commande { LignesCommande = new List<LigneCommande>() };

        // Configuration de la fenêtre
        this.Text = "Créer une Commande";
        this.Size = new Size(750, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;

        // En-tête stylisé
        var pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 50,
            BackColor = Color.FromArgb(52, 73, 94)
        };
        var lblTitre = new Label
        {
            Text = "Créer une Commande",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        pnlHeader.Controls.Add(lblTitre);
        this.Controls.Add(pnlHeader);

        // Conteneur principal avec FlowLayoutPanel
        var flowLayout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            AutoScroll = true,
            Padding = new Padding(10),
            WrapContents = false
        };

        // Ajout des champs Client, Article, Quantité
        flowLayout.Controls.Add(CreateField("Client :", out cboClients));
        cboClients.DataSource = clientDAO.RecupererTousLesClient();
        cboClients.DisplayMember = "Nom";
        cboClients.ValueMember = "Id";

        flowLayout.Controls.Add(CreateField("Article :", out cboArticles));
        cboArticles.DataSource = articleDAO.RecupererTousLesArticles();
        cboArticles.DisplayMember = "Nom";
        cboArticles.ValueMember = "Id";

        flowLayout.Controls.Add(CreateField("Quantité :", out txtQuantite));
        txtQuantite.PlaceholderText = "Entrez une quantité";

        // Bouton pour ajouter un article
        btnAjouterArticle = new Button
        {
            Text = "Ajouter Article",
            BackColor = Color.FromArgb(52, 152, 219),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            Height = 40,
            Width = 200
        };
        btnAjouterArticle.FlatAppearance.BorderSize = 0;
        btnAjouterArticle.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
        btnAjouterArticle.Click += BtnAjouterArticle_Click;
        flowLayout.Controls.Add(btnAjouterArticle);

        // DataGridView pour les articles
        dgvArticles = new DataGridView
        {
            Dock = DockStyle.Top,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ColumnCount = 4,
            Height = 200
        };
        dgvArticles.Columns[0].Name = "Article";
        dgvArticles.Columns[1].Name = "Quantité";
        dgvArticles.Columns[2].Name = "Prix Unitaire";
        dgvArticles.Columns[3].Name = "Total";
        flowLayout.Controls.Add(dgvArticles);

        this.Controls.Add(flowLayout);

        // Bouton de validation
        btnValiderCommande = new Button
        {
            Text = "Valider Commande",
            BackColor = Color.FromArgb(46, 204, 113),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            Height = 40,
            Dock = DockStyle.Bottom
        };
        btnValiderCommande.FlatAppearance.BorderSize = 0;
        btnValiderCommande.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 174, 96);
        btnValiderCommande.Click += BtnValiderCommande_Click;
        this.Controls.Add(btnValiderCommande);
    }

    // Méthode pour créer un champ avec un label et un contrôle (TextBox/ComboBox)
    private Panel CreateField(string labelText, out ComboBox comboBox)
    {
        var fieldPanel = new Panel
        {
            Width = 600,
            Height = 60
        };

        var lbl = new Label
        {
            Text = labelText,
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            ForeColor = Color.FromArgb(52, 73, 94),
            Width = 200,
            TextAlign = ContentAlignment.MiddleLeft
        };

        comboBox = new ComboBox
        {
            Font = new Font("Segoe UI", 10),
            Width = 380
        };

        fieldPanel.Controls.Add(lbl);
        fieldPanel.Controls.Add(comboBox);

        return fieldPanel;
    }

    private Panel CreateField(string labelText, out TextBox textBox)
    {
        var fieldPanel = new Panel
        {
            Width = 600,
            Height = 60
        };

        var lbl = new Label
        {
            Text = labelText,
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            ForeColor = Color.FromArgb(52, 73, 94),
            Width = 200,
            TextAlign = ContentAlignment.MiddleLeft
        };

        textBox = new TextBox
        {
            Font = new Font("Segoe UI", 10),
            Width = 380
        };

        fieldPanel.Controls.Add(lbl);
        fieldPanel.Controls.Add(textBox);

        return fieldPanel;
    }

    private void BtnAjouterArticle_Click(object sender, EventArgs e)
    {
        try
        {
            int articleId = (int)cboArticles.SelectedValue;
            int quantite = int.Parse(txtQuantite.Text);
            Article article = articleDAO.RecupererArticleParId(articleId);
            decimal prixUnitaire = article.Prix;

            commande.LignesCommande.Add(new LigneCommande
            {
                ArticleId = articleId,
                NomArticle = article.Nom,
                Quantite = quantite,
                PrixUnitaire = prixUnitaire
            });

            dgvArticles.Rows.Add(article.Nom, quantite, prixUnitaire, quantite * prixUnitaire);

            txtQuantite.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur lors de l'ajout de l'article : {ex.Message}");
        }
    }

    private void BtnValiderCommande_Click(object sender, EventArgs e)
    {
        try
        {
            commande.ClientId = (int)cboClients.SelectedValue;
            commande.DateCommande = DateTime.Now;
            commande.Statut = "En cours";

            CommandeDAO commandeDAO = new CommandeDAO();
            bool isSuccess = commandeDAO.AjouterCommande(commande);

            if (isSuccess)
            {
                MessageBox.Show("Commande enregistrée avec succès !");
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Erreur lors de l'enregistrement de la commande.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur lors de la validation de la commande : {ex.Message}");
        }
    }
}
