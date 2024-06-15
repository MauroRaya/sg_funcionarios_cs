﻿using sg_funcionarios.BLL;
using sg_funcionarios.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sg_funcionarios
{
    public partial class FormFuncionario : Form
    {
        public FormFuncionario()
        {
            InitializeComponent();
            this.Load += loadFuncionarios;
            dataGridView1.CellContentClick += clicarImagens;
        }

        private void clicarImagens(object sender, DataGridViewCellEventArgs e) //de editar e remover
        {
            String colname = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colname == "editar")
            {
                FormFuncionarioEdit formFuncEdit = new FormFuncionarioEdit();
                formFuncEdit.Show();
                this.Hide();
            }
            else if (colname == "remover")
            {
                //fazer ainda
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Bitmap GetThumbnailImage(String path, int width, int height)
        {
            using (Bitmap originalImage = new Bitmap(path))
            {
                return new Bitmap(originalImage.GetThumbnailImage(width, height, null, IntPtr.Zero));
            }
        }

        private void loadFuncionarios(object sender, EventArgs e)
        {
            var listaFuncionarios = FuncionarioBLL.getFuncionarios();

            if (listaFuncionarios == null)
            {
                MessageBox.Show(Erro.getMsgErro());
                return;
            }

            String imgEditarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"imgs\img_editar.png");
            String imgRemoverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"imgs\img_remover.png");

            Bitmap imgEditar = GetThumbnailImage(imgEditarPath, 22, 22);
            Bitmap imgRemover = GetThumbnailImage(imgRemoverPath, 22, 22);

            foreach (var funcionario in listaFuncionarios)
            {
                dataGridView1.Rows.Add(
                    funcionario.getCodigo(),
                    funcionario.getNome(),
                    funcionario.getDataNascimento(),
                    funcionario.getGenero(),
                    funcionario.getTelefone(),
                    funcionario.getCargo(),
                    funcionario.getSalario(),
                    imgEditar,
                    imgRemover
                );
            }
        }

        private void btnAddFuncionario_Click(object sender, EventArgs e)
        {
            FormFuncionarioAdd formFuncAdd = new FormFuncionarioAdd();
            formFuncAdd.Show();
            this.Hide();
        }
    }
}
