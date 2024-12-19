namespace MockProject.Browsers;

public interface IBrowser
{
    /// <summary>
    /// Navigate to the URL
    /// </summary>
    /// <param name="url">The URL to navigate to</param>
    void GoToUrl(string url);

    /// <summary>
    /// Get the web element
    /// </summary>
    /// <param name="by">The locator of the web element</param>
    /// <returns>
    /// The web element if found
    /// </returns>
    IWebElement GetWebElement(By by);

    /// <summary>
    /// Get the web elements
    /// </summary>
    /// <param name="by">The locator of the web elements</param>
    /// <returns>
    /// The web elements if found otherwise return empty list
    /// </returns>
    IEnumerable<IWebElement> GetWebElements(By by);

    /// <summary>
    /// Try to get the web element until the timeout (in miliseconds) is reached
    /// </summary>
    /// <param name="by">The locator of the web element</param>
    /// <param name="timeout">The timeout in miliseconds</param>
    /// <returns>
    /// The web element if found
    /// </returns>
    IWebElement TryGetWebElementUntil(By by, int timeout);

    /// <summary>
    /// Try to get the web elements until the timeout (in miliseconds) is reached
    /// </summary>
    /// <param name="by">The locator of the web elements</param>
    /// <param name="timeout">The timeout in miliseconds</param>
    /// <returns>
    /// The web elements if found, otherwise return empty list
    /// </returns>
    IEnumerable<IWebElement> TryGetWebElementsUntil(By by, int timeout);

    /// <summary>
    /// Quit the browser
    /// </summary>
    void Quit();
}
