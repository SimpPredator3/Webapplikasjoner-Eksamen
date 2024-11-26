import React from "react";

class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false, error: null, errorInfo: null };
  }

  static getDerivedStateFromError(error) {
    return { hasError: true, error };
  }

  componentDidCatch(error, errorInfo) {
    this.setState({ error, errorInfo });
    console.error("Error caught by ErrorBoundary:", error, errorInfo); // Logging
  }

  render() {
    if (this.state.hasError) {
      return (
        <div>
          <h1>Something went wrong.</h1>
          <details>
            <summary>Details</summary>
            <p>{this.state.error?.toString()}</p>
          </details>
        </div>
      );
    }
    return this.props.children;
  }
}

export default ErrorBoundary;
