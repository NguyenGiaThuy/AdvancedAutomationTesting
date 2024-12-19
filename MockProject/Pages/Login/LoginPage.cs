namespace MockProject.Pages.Login;

public class LoginPage : PageBase
{
    #region PageElements
    private IWebElement _usernameInput =>
        _webDriver.FindElement(By.XPath("//input[@name='username']"));
    private IWebElement _passwordInput =>
        _webDriver.FindElement(By.XPath("//input[@name='password']"));
    private IWebElement _loginButton =>
        _webDriver.FindElement(By.ClassName("orangehrm-login-button"));
    #endregion

    #region PageInteractions
    public LoginPage(IWebDriver webDriver, WebDriverWait wait)
        : base(
            webDriver,
            wait,
            "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login"
        ) { }

    public void EnterUsername(string username)
    {
        _usernameInput.SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        _passwordInput.SendKeys(password);
    }

    public void ClickLoginButton()
    {
        _loginButton.Click();
    }

    public void LoginUser(string username, string password)
    {
        EnterUsername(username);
        EnterPassword(password);
        ClickLoginButton();
    }
    #endregion
}
