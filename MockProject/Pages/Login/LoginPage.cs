namespace MockProject.Pages.Login;

public class LoginPage(IBrowser browser)
    : PageBase(browser, "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login")
{
    #region PageElements
    private By _usernameInput = By.XPath("//input[@name='username']");
    private By _passwordInput = By.XPath("//input[@name='password']");
    private By _loginButton = By.ClassName("orangehrm-login-button");
    #endregion

    #region PageInteractions
    public void EnterUsername(string username)
    {
        var usernameInput = _browser.GetWebElement(_usernameInput);
        usernameInput.SendKeys(username);
    }

    public void EnterPassword(string password)
    {
        var passwordInput = _browser.GetWebElement(_passwordInput);
        passwordInput.SendKeys(password);
    }

    public void ClickLoginButton()
    {
        var loginButton = _browser.GetWebElement(_loginButton);
        loginButton.Click();
    }

    public void LoginUser(string username, string password)
    {
        EnterUsername(username);
        EnterPassword(password);
        ClickLoginButton();
    }
    #endregion
}
