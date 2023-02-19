using System.ComponentModel;

namespace WalletApi.Enums
{
    public enum OperationType
    {
        [Description("Withdraw")]
        Withdraw,

        [Description("Deposit")]
        Deposit
    }
}
