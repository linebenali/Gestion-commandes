using System;
using System.Drawing;
using System.Windows.Forms;

public class ArticlesForm : Form
{
    private DataGridView dgvArticles; // Tableau pour afficher les articles
    private Button btnAjouter, btnModifier, btnSupprimer;
    private Label lblTitre;
    private Panel pnlHeader, pnlFooter, pnlBody;

    private ArticleDAO articleDAO;

    public ArticlesForm()
    {
        // Initialisation de la DAO
        articleDAO = new ArticleDAO();

        // Configuration de la fenêtre
        this.Text = "Gestion des Articles";
        this.Size = new Size(900, 600);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.BackColor = Color.White;

        // En-tête
        pnlHeader = new Panel
        {
            Dock = DockStyle.Top,
            Height = 70,
            BackColor = Color.FromArgb(52, 73, 94)
        };

        lblTitre = new Label
        {
            Text = "Gestion des Articles",
            Font = new Font("Segoe UI", 20, FontStyle.Bold),
            ForeColor = Color.White,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter
        };
        pnlHeader.Controls.Add(lblTitre);

        // Corps principal
        pnlBody = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(20)
        };

        // Configuration du DataGridView
        dgvArticles = new DataGridView
        {
            Dock = DockStyle.Top,
            Height = 350,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            },
            DefaultCellStyle = new DataGridViewCellStyle
            {
                SelectionBackColor = Color.FromArgb(41, 128, 185),
                SelectionForeColor = Color.White
            }
        };

        // Pied de page avec FlowLayoutPanel pour les boutons
        pnlFooter = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 80,
            BackColor = Color.White,
            Padding = new Padding(10)
        };
        var flowLayoutPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            AutoSize = true,
            Padding = new Padding(10),
            WrapContents = false
        };

        btnAjouter = CreateButton("Ajouter", Color.SeaGreen, Color.MediumSeaGreen, BtnAjouter_Click);
        btnModifier = CreateButton("Modifier", Color.SeaGreen, Color.MediumSeaGreen, BtnModifier_Click);
        btnSupprimer = CreateButton("Supprimer", Color.SeaGreen, Color.MediumSeaGreen, BtnSupprimer_Click);

        flowLayoutPanel.Controls.Add(btnAjouter);
        flowLayoutPanel.Controls.Add(btnModifier);
        flowLayoutPanel.Controls.Add(btnSupprimer);

        pnlFooter.Controls.Add(flowLayoutPanel);

        // Ajout des composants
        pnlBody.Controls.Add(dgvArticles);
        this.Controls.Add(pnlBody);
        this.Controls.Add(pnlFooter);
        this.Controls.Add(pnlHeader);

        // Charger les données initiales
        ChargerArticles();
    }

    private Button CreateButton(string text, Color backColor, Color hoverBackColor, EventHandler clickEvent)
    {
        var button = new Button
        {
            Text = text,
            Width = 120,
            Height = 40,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            BackColor = backColor,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(5),
            Cursor = Cursors.Hand
        };
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = hoverBackColor;
        button.Click += clickEvent;
        return button;
    }

    // Charger les articles dans la DataGridView
    private void ChargerArticles()
    {
        var articles = articleDAO.RecupererTousLesArticles();
        dgvArticles.DataSource = articles;
    }

    // Événement : Ajouter un article
    private void BtnAjouter_Click(object sender, EventArgs e)
    {
        var formAjout = new ArticleForm();
        if (formAjout.ShowDialog() == DialogResult.OK)
        {
            if (articleDAO.AjouterArticle(formAjout.Article))
            {
                MessageBox.Show("Article ajouté avec succès !");
                ChargerArticles();
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout de l'article.");
            }
        }
    }

    // Événement : Modifier un article
    private void BtnModifier_Click(object sender, EventArgs e)
    {
        if (dgvArticles.CurrentRow != null)
        {
            var article = (Article)dgvArticles.CurrentRow.DataBoundItem;
            var formModifier = new ArticleForm(article);
            if (formModifier.ShowDialog() == DialogResult.OK)
            {
                if (articleDAO.ModifierArticle(formModifier.Article))
                {
                    MessageBox.Show("Article modifié avec succès !");
                    ChargerArticles();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la modification de l'article.");
                }
            }
        }
    }

    // Événement : Supprimer un article
    private void BtnSupprimer_Click(object sender, EventArgs e)
    {
        if (dgvArticles.CurrentRow != null)
        {
            var article = (Article)dgvArticles.CurrentRow.DataBoundItem;
            if (MessageBox.Show($"Voulez-vous supprimer l'article : {article.Nom} ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (articleDAO.SupprimerArticle(article.Id))
                {
                    MessageBox.Show("Article supprimé avec succès !");
                    ChargerArticles();
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression de l'article.");
                }
            }
        }
    }
}
