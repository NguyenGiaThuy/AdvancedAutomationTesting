namespace MockProject.Pages;

public class LoginPage : PageBase
{
    IWebElement _usernameInput => _webDriver.FindElement(By.XPath("//input[@name='username']"));
    IWebElement _passwordInput => _webDriver.FindElement(By.XPath("//input[@name='password']"));
    IWebElement _loginButton => _webDriver.FindElement(By.ClassName("orangehrm-login-button"));

    public LoginPage(IWebDriver webDriver, WebDriverWait wait, string url)
        : base(webDriver, wait, url) { }

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
}
