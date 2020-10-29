using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    public class cashFactory
    {
    }

    public abstract class cashBase
    {
        public abstract double AcceptMoney(double money);
    }


    public class cashRebate : cashBase
    {
        private double _rebate = 0.0;
        public cashRebate(double moneyRebate)
        {
            if (moneyRebate < 0 || moneyRebate > 1)
            {
                moneyRebate = 1;
            }
            _rebate = moneyRebate;
        }
        public override double AcceptMoney(double money)
        {
            return _rebate * money;
        }
    }

    public class cashRetun : cashBase
    {
        private double moneyCondition = 0.0;
        private double moneyReturn = 0.0;
        public cashRetun(double MoneyCondition ,double MoneyReturn)
        {
            moneyCondition = MoneyCondition;
            moneyReturn = MoneyReturn;
        }

        public override double AcceptMoney(double money)
        {
            if (money >= moneyCondition)
            {
                return money - Math.Floor(money / moneyCondition) * moneyReturn;
            }
            return money;
        }
    }
}
