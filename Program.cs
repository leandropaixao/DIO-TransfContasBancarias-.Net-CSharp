using System;
using System.Linq;
using System.Collections.Generic;
using DIO_TransfContasBancarias_.Net_CSharp.Model;

namespace DIO_TransfContasBancarias_.Net_CSharp
{
    class Program
    {
        private static List<Conta> _contas = new List<Conta>();
        static void Main(string[] args)
        {
            /* descomentar as linhas a baixo para preenchimento automático no cadastro */
            //_contas.Add(new Conta(nome:"Leandro", saldo:590.00, credito:300.00, tipoPessoa: Enum.TipoPessoa.PessoaFisica));
            //_contas.Add(new Conta(nome:"Astolfo", saldo:20.00, credito:0.00, tipoPessoa: Enum.TipoPessoa.PessoaJuridica));
            
            var continuar = "";
            Console.Clear();
            do
            {   
                montarMenu();
                continuar = Console.ReadLine();
                var msg = "";

                switch(continuar.ToUpper())
                {
                    case "1":
                        Console.Clear();
                        cadastrarConta();
                        Console.WriteLine("[[ CADASTRO REALIZADO COM SUCESSO ]] \n");
                        break;
                    case "2":
                        Console.Clear();
                        msg = realizarDeposito() != true ? "[[ OPERAÇÃO CANCELADA - VERIFIQUE SEU SALDO ]] \n" : "[[ DEPÓSITO REALIZADO COM SUCESSO ]] \n";
                        Console.WriteLine(msg);
                        break;
                    case "3":
                        Console.Clear();
                        msg = realizarSaque() != true ? "[[ OPERAÇÃO CANCELADA - VERIFIQUE SEU SALDO ]] \n" : "[[ SAQUE REALIZADO COM SUCESSO ]] \n";
                        Console.WriteLine(msg);
                        break;
                    case "4":
                        Console.Clear();
                        msg = realizarTransferencia() != true ? "[[ OPERAÇÃO CANCELADA - VERIFIQUE SEU SALDO ]] \n" : "[[ TREANSFERÊNCIA REALIZADA COM SUCESSO ]] \n";
                        Console.WriteLine(msg);
                        break;
                    case "5":
                        Console.Clear();
                        foreach(var item in _contas)
                        {
                            Console.WriteLine(item);
                        }
                        Console.WriteLine("");
                        break;
                    case "X":
                        Console.WriteLine("Obrigado por usar nossos serviços [DIO Banque]");
                        break;
                    default: 
                        Console.WriteLine("Opção inválida. Digite novamente");
                        break;
                }
                
            } while(continuar.ToUpper() != "X");
        }

        private static bool realizarTransferencia()
        {
            Console.Write("\n === REALIZAR TRANSFERÊNCIA ===\n");
            
            Console.Write("Informe o número da conta a ser debitada: ");
            var numConta = Int32.Parse(Console.ReadLine());
            
            Console.Write("Informe o número da conta a ser creditada: ");
            var numContaTrans = Int32.Parse(Console.ReadLine());
            
            Console.Write("Informe o valor a ser transferido: ");
            double valor = Double.Parse(Console.ReadLine());

            return (validarConta(numConta) && validarConta(numContaTrans)) ? _contas[numConta].transferir(valorTransf: valor, conta: _contas[numContaTrans]) : false;   
        }

        private static bool realizarSaque()
        {
            Console.Write("\n === REALIZAR SAQUE ===\n");
            
            Console.Write("Informe o número da conta: ");
            var numConta = Int32.Parse(Console.ReadLine());
            
            Console.Write("Informe o valor a ser sacado: ");
            double valor = Double.Parse(Console.ReadLine());

            return validarConta(numConta) ? _contas[numConta].sacar(valor) : false;
        }

        private static bool realizarDeposito()
        {
            Console.Write("\n === REALIZAR DEPÓSITO ===\n");
            
            Console.Write("Informe o número da conta: ");
            var numConta = Int32.Parse(Console.ReadLine());
            
            Console.Write("Informe o valor a ser depositado: ");
            double valor = Double.Parse(Console.ReadLine());

            return validarConta(numConta) ? _contas[numConta].depositar(valor): false;
        }

        private static void cadastrarConta()
        {   
            Console.Write("\n === CADASTRO DE CONTA ===\n");
            Console.Write("Informe o nome da pessoa: ");
            string nome = Console.ReadLine();

            Console.Write("Informe o tipo de pessoa [1] Física [2] Jurídica: ");
            int tipoPessoa = Int16.Parse(Console.ReadLine());

            Console.Write("Informe o saldo inicial: ");
            double saldo = Double.Parse(Console.ReadLine());

            Console.Write("Informe o valor de crédito: ");
            double credito = Double.Parse(Console.ReadLine());

            _contas.Add(new Conta(nome: nome, saldo: saldo, credito : credito, tipoPessoa: (Enum.TipoPessoa) tipoPessoa));    
        }

        static void montarMenu()
        {
            Console.WriteLine("===== DIO Banque =====");
            Console.WriteLine("1 - Cadastrar conta");
            Console.WriteLine("2 - Depositar");
            Console.WriteLine("3 - Sacar");
            Console.WriteLine("4 - Transferir");
            Console.WriteLine("5 - Listas contas");
            Console.WriteLine("X - Sair");
        }

        static bool validarConta(int numConta)
        {
            if (numConta > _contas.Count)
            {
                Console.Clear();
                Console.WriteLine("Conta inexistente - Repita a operação");
                return false;
            }
            return true;
        }
    }
}
