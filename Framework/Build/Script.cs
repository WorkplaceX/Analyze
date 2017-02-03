namespace Build
{
    public class Script : Framework.Build.ScriptBase
    {
        public override void RunSql()
        {
            Airport.Script.Run();
            base.RunSql();
        }
    }
}