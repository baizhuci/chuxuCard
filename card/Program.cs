using System;
class XinyongCard
{
     private static float SumMoney=100000;//总额度
    private static float MonthMoney=0;//月消费总额
    private static int date;//还款日
    public static float ShowMonthMoney()
    {
        return MonthMoney;
    }
    public static int ShowDate()
    { return date; }
     public void SpendMoney(float money)
    {
        MonthMoney += money;
        Console.WriteLine("您的信用卡本次消费{0}元！",money);
    }
    public void ChangeDate(int date)
    { XinyongCard.date = date; }
    public void MonthMenu()
    {
        Console.WriteLine("本月账单：\n    截至{0}日，本月您共消费{1}元！",date,MonthMoney);
    }
    public static void Back()
    {
        MonthMoney = 0;
    }
}
class ChuxuCard
{
    float MyMoney = 0;
    public void SaveMoney(float money)
    {
        if (money > 0)
            Console.WriteLine("您本次存储{0}元！", money);
        else
            Console.WriteLine("您本次消费{0}元！", -money);
        MyMoney += money;
    }
    public void Back()
    {
        if (MyMoney < XinyongCard.ShowMonthMoney())
            Console.WriteLine("余额不足，还款失败！");
        else
        {
            Console.WriteLine("储蓄卡还款成功！");
            MyMoney = MyMoney - XinyongCard.ShowMonthMoney();
            XinyongCard.Back();
        }
    }
    public void ShowMyMoney()
    {
        Console.WriteLine("您的储蓄卡内有{0}元！", MyMoney);
    }
}
class MyCard
{
    public XinyongCard xinyongCard=new XinyongCard();
    ChuxuCard[] chuxuCards;
    public MyCard(int num)
    {
        chuxuCards = new ChuxuCard[num];
    }
    public ChuxuCard this[int index]
    {
        get
        {
            if(index<0||index>=chuxuCards.Length)
            {
                Console.WriteLine("寻卡失败！");
                return null;
            }
            return chuxuCards[index];
        }
        set
        {
            if (index >= chuxuCards.Length||index<0)
            {
                Console.WriteLine("寻卡失败！");
                return;
            }
            chuxuCards[index] = value;
        }
    }
}
class Delegate
{
    public delegate void BackMoney();
    public event BackMoney NotifyBack;
    public void Notify(int date)
    {
        if (NotifyBack != null && date == XinyongCard.ShowDate())
        {
            Console.WriteLine("到还款日期了！");
            NotifyBack();
        }
        else
            Console.WriteLine("未到还款日期！");
    }
}
class BackSystem
{
    static void Main()
    {
        MyCard myCard = new MyCard(3);
        ChuxuCard one = new ChuxuCard();
        ChuxuCard two = new ChuxuCard();
        ChuxuCard three = new ChuxuCard();
        myCard[0]=one;
        myCard[1]=two;
        myCard[2]=three;
        //卡里现在有一张信用卡和三张储蓄卡
        myCard.xinyongCard.SpendMoney(78000);
        //信用卡消费
        myCard[0].SaveMoney(9000);
        myCard[1].SaveMoney(80000);
        myCard[2].SaveMoney(1000);
        //储蓄卡存款
        myCard.xinyongCard.ChangeDate(27);
        //设置还款日
        Delegate back = new Delegate();
        back.NotifyBack += new Delegate.BackMoney(myCard.xinyongCard.MonthMenu);
        back.NotifyBack += new Delegate.BackMoney(one.Back);
        back.Notify(27);
    }
}