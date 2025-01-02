namespace TestCore.Browsers;

public interface IBrowser
{
    /// <summary>
    /// Navigates the browser to the specified URL.
    /// </summary>
    /// <param name="url">The URL to navigate to.</param>
    void GoToUrl(string url);

    /// <summary>
    /// Finds and retrieves a single web element using the specified locator.
    /// </summary>
    /// <param name="by">The locator used to find the web element.</param>
    /// <returns>The web element if found; otherwise, throws an exception.</returns>
    IWebElement GetWebElement(By by);

    /// <summary>
    /// Finds and retrieves all web elements matching the specified locator.
    /// </summary>
    /// <param name="by">The locator used to find the web elements.</param>
    /// <returns>
    /// A collection of web elements if found; otherwise, an empty list.
    /// </returns>
    IEnumerable<IWebElement> GetWebElements(By by);

    /// <summary>
    /// Attempts to find a single web element using the specified locator, retrying until the timeout is reached.
    /// </summary>
    /// <param name="by">The locator used to find the web element.</param>
    /// <param name="timeout">The timeout in milliseconds to wait for the element to appear.</param>
    /// <returns>
    /// The web element if found within the timeout; otherwise, throws an exception.
    /// </returns>
    IWebElement TryGetWebElementUntil(By by, int timeout);

    /// <summary>
    /// Attempts to find all web elements matching the specified locator, retrying until the timeout is reached.
    /// </summary>
    /// <param name="by">The locator used to find the web elements.</param>
    /// <param name="timeout">The timeout in milliseconds to wait for the elements to appear.</param>
    /// <returns>
    /// A collection of web elements if found within the timeout; otherwise, an empty list.
    /// </returns>
    IEnumerable<IWebElement> TryGetWebElementsUntil(By by, int timeout);

    /// <summary>
    /// Checks whether the web element specified by the locator is visible within the timeout period.
    /// </summary>
    /// <param name="by">The locator used to find the web element.</param>
    /// <param name="timeout">The timeout in milliseconds to wait for visibility. Defaults to 1000ms.</param>
    /// <returns>
    /// <c>true</c> if the web element is visible within the timeout; otherwise, <c>false</c>.
    /// </returns>
    bool WebElementIsVisibile(By by, int timeout = 1000);

    /// <summary>
    /// Checks whether the web element specified by the locator is clickable within the timeout period.
    /// </summary>
    /// <param name="by">The locator used to find the web element.</param>
    /// <param name="timeout">The timeout in milliseconds to wait for clickability. Defaults to 1000ms.</param>
    /// <returns>
    /// <c>true</c> if the web element is clickable within the timeout; otherwise, <c>false</c>.
    /// </returns>
    bool WebElementIsClickable(By by, int timeout = 1000);

    /// <summary>
    /// Closes the browser and cleans up resources.
    /// </summary>
    void Quit();
}
