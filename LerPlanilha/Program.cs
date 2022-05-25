

using System;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

class ReadingCSV
{

    public static void Main()
    {

        string nome;
        string periodo;
        string categoria;
        string dificuldade;
        string creditos;
        string horaAula;
        string horaRelogio;
        string qtdTeorica;
        string qtdPratica;
        string ementa;
        string preRequisito;
        string necessariaPara;
        string disciplina_id;
        var path = @"C:\Data\disciplinas.csv";
        var path2 = @"C:\Data\prerequisito.csv";
        using (TextFieldParser csvReader = new TextFieldParser(path))
        {
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { ";" });
            csvReader.HasFieldsEnclosedInQuotes = true;
            csvReader.ReadLine();
            //Iniciando conexão
            MySqlConnection mConn;
            MySqlDataAdapter mAdapter;
            
        mConn = new MySqlConnection("Persist Security Info=False; server=localhost;database=local;uid=felipe;password=Acesso@01");

            // Abre a conexão
            mConn.Open();

            while (!csvReader.EndOfData)
            {
                
                string[] fields = csvReader.ReadFields();
                nome = fields[0];
                periodo = fields[1];
                categoria = fields[2];
                dificuldade = fields[3];
                creditos = fields[4];
                horaAula = fields[5];
                horaRelogio = fields[6];
                qtdTeorica = fields[7];
                qtdPratica = fields[8];
                ementa = fields[9];



                //MySqlCommand command = new MySqlCommand("INSERT INTO disciplinas(disciplina_nome, disciplina_periodo, disciplina_categoria, disciplina_dificuldade, disciplina_creditos, disciplina_horaAula, disciplina_horaRelogio, disciplina_qtdTeorica, disciplina_qtdPratica, disciplina_ementa)" +
                //"VALUES('" + nome + "','" + periodo + "','" + categoria + "','" + dificuldade + "','" + creditos + "','" + horaAula + "','" + horaRelogio + "','" + qtdTeorica + "','" + qtdPratica + "','" + ementa + "')", mConn);

                //Executa a Query SQL
                //command.ExecuteNonQuery();

                

            }
            mConn.Close();
        }
        using (TextFieldParser csvReader = new TextFieldParser(path2))
        {
            csvReader.CommentTokens = new string[] { "#" };
            csvReader.SetDelimiters(new string[] { ";" });
            csvReader.HasFieldsEnclosedInQuotes = true;
            csvReader.ReadLine();
            //Iniciando conexão
            MySqlConnection mConn;
            MySqlDataAdapter mAdapter;
            DataSet mDataSet;
            mDataSet = new DataSet();
            mConn = new MySqlConnection("Persist Security Info=False; server=localhost;database=local;uid=felipe;password=Acesso@01");

            // Abre a conexão
            mConn.Open();

            while (!csvReader.EndOfData)
            {

                string[] fields = csvReader.ReadFields();
                nome = fields[1];
                preRequisito = fields[2];
                periodo = fields[3];
                creditos = fields[4];
                necessariaPara = fields[5];
                disciplina_id = "0";
                MySqlCommand selectDisciplina = new MySqlCommand("SELECT distinct * FROM disciplinas WHERE disciplina_nome like '"+ nome + "'", mConn);
                using MySqlDataReader dcp = selectDisciplina.ExecuteReader();
                while (dcp.Read())
                {
                    disciplina_id =$"{dcp.GetString(0),-10}";
                }
                mConn.Close();
                mConn.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO prerequisitos(disciplina_id, disciplina_nome, requisitos_disciplina, requisitos_periodo, requisitos_creditos, requisitos_nescessariaPara)" +
                "VALUES('" + disciplina_id + "','" + nome + "','" + preRequisito + "','" + periodo + "','" + creditos + "','" + necessariaPara + "')", mConn);

                //Executa a Query SQL
                command.ExecuteNonQuery();


            }
            mConn.Close();
        }

    }
}