namespace WindowsFormsTest
{
    partial class FormTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTesteRest_RegistroBilheteriaSalaExibicao = new System.Windows.Forms.Button();
            this.btn_ProtocolosUmDiaCinematografico = new System.Windows.Forms.Button();
            this.btn_ConsultaAdimplencia = new System.Windows.Forms.Button();
            this.btn_ConsultaProtocoloPorID = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTesteRest_RegistroBilheteriaSalaExibicao
            // 
            this.btnTesteRest_RegistroBilheteriaSalaExibicao.Location = new System.Drawing.Point(13, 13);
            this.btnTesteRest_RegistroBilheteriaSalaExibicao.Name = "btnTesteRest_RegistroBilheteriaSalaExibicao";
            this.btnTesteRest_RegistroBilheteriaSalaExibicao.Size = new System.Drawing.Size(302, 68);
            this.btnTesteRest_RegistroBilheteriaSalaExibicao.TabIndex = 0;
            this.btnTesteRest_RegistroBilheteriaSalaExibicao.Text = "1 - Test - Registro de Bilheteria de Sala de Exibição";
            this.btnTesteRest_RegistroBilheteriaSalaExibicao.UseVisualStyleBackColor = true;
            this.btnTesteRest_RegistroBilheteriaSalaExibicao.Click += new System.EventHandler(this.btnTesteRest_RegistroBilheteriaSalaExibicao_Click);
            // 
            // btn_ProtocolosUmDiaCinematografico
            // 
            this.btn_ProtocolosUmDiaCinematografico.Location = new System.Drawing.Point(12, 161);
            this.btn_ProtocolosUmDiaCinematografico.Name = "btn_ProtocolosUmDiaCinematografico";
            this.btn_ProtocolosUmDiaCinematografico.Size = new System.Drawing.Size(302, 68);
            this.btn_ProtocolosUmDiaCinematografico.TabIndex = 1;
            this.btn_ProtocolosUmDiaCinematografico.Text = "3 - Test - Consulta Protocolos de Um Dia Cinamatográfico";
            this.btn_ProtocolosUmDiaCinematografico.UseVisualStyleBackColor = true;
            this.btn_ProtocolosUmDiaCinematografico.Click += new System.EventHandler(this.btn_ProtocolosUmDiaCinematografico_Click);
            // 
            // btn_ConsultaAdimplencia
            // 
            this.btn_ConsultaAdimplencia.Location = new System.Drawing.Point(13, 235);
            this.btn_ConsultaAdimplencia.Name = "btn_ConsultaAdimplencia";
            this.btn_ConsultaAdimplencia.Size = new System.Drawing.Size(302, 68);
            this.btn_ConsultaAdimplencia.TabIndex = 2;
            this.btn_ConsultaAdimplencia.Text = "4 - Test - Consulta Adimplencia";
            this.btn_ConsultaAdimplencia.UseVisualStyleBackColor = true;
            this.btn_ConsultaAdimplencia.Click += new System.EventHandler(this.btn_ConsultaAdimplencia_Click);
            // 
            // btn_ConsultaProtocoloPorID
            // 
            this.btn_ConsultaProtocoloPorID.Location = new System.Drawing.Point(13, 87);
            this.btn_ConsultaProtocoloPorID.Name = "btn_ConsultaProtocoloPorID";
            this.btn_ConsultaProtocoloPorID.Size = new System.Drawing.Size(302, 68);
            this.btn_ConsultaProtocoloPorID.TabIndex = 3;
            this.btn_ConsultaProtocoloPorID.Text = "2 - Test - Consulta Protocolo por ID";
            this.btn_ConsultaProtocoloPorID.UseVisualStyleBackColor = true;
            this.btn_ConsultaProtocoloPorID.Click += new System.EventHandler(this.btn_ConsultaProtocoloPorID_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 319);
            this.Controls.Add(this.btn_ConsultaProtocoloPorID);
            this.Controls.Add(this.btn_ConsultaAdimplencia);
            this.Controls.Add(this.btn_ProtocolosUmDiaCinematografico);
            this.Controls.Add(this.btnTesteRest_RegistroBilheteriaSalaExibicao);
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTesteRest_RegistroBilheteriaSalaExibicao;
        private System.Windows.Forms.Button btn_ProtocolosUmDiaCinematografico;
        private System.Windows.Forms.Button btn_ConsultaAdimplencia;
        private System.Windows.Forms.Button btn_ConsultaProtocoloPorID;
    }
}

