namespace web_gualan.Helpers
{
    public static class AlertHelper
    {
        public static string Success(string msg) =>
            $"<div class='alert alert-success'>{msg}</div>";

        public static string Error(string msg) =>
            $"<div class='alert alert-danger'>{msg}</div>";
    }
}
