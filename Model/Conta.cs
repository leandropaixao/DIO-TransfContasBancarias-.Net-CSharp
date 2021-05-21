using System;
using DIO_TransfContasBancarias_.Net_CSharp.Enum;

namespace DIO_TransfContasBancarias_.Net_CSharp.Model
{
    public class Conta
    {
        private TipoPessoa _tipoPessoa;
        private string _nome;
        private double _saldo;
        private double _credito;
        
        public Conta(string nome, double saldo, double credito, TipoPessoa tipoPessoa)
        {
            this._nome = nome;
            this._saldo = saldo;
            this._credito = credito;
            this._tipoPessoa = tipoPessoa;
        }

        public override string ToString()
        {
            return $"Nome: {_nome} | Tipo: {_tipoPessoa} | Saldo R$: {_saldo.ToString("N2")} | Crédito R$: {_credito.ToString("N2")}";
        }

        public bool depositar(double valor)
        {
            try
            {
                _saldo += valor;
            }
            catch
            {   
                return false;
            }
            return true;
        }

        public bool sacar(double valor)
        {
            try
            {
                if (_saldo < valor)
                    return false;

                _saldo -= valor;
            }
            catch 
            {
                return false;
            }
            return true;
        }

        public bool transferir(double valorTransf, Conta conta)
        {
            try
            {
                if (!sacar(valorTransf))
                    return false;
                
                conta.depositar(valorTransf);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}