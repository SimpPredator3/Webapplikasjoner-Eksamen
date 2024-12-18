import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import Container from 'react-bootstrap/Container';
import HomePage from './home/HomePage';
import NavMenu from './shared/NavMenu';
import PostListPage from './posts/PostListPage';
import AdminListPage from './admindashboard/AdminListPage';
import LoginModalComponent from "./components/LoginModalComponent";
import { UserProvider } from './components/UserContext';

function App() {
  return (
    <UserProvider>
      <Router>
        <Container>
          <NavMenu />
          <LoginModalComponent />
          <Routes>
            <Route path="/" element={<HomePage />} />
            <Route path="/posts" element={<PostListPage />} />
            <Route path="/AdminDash" element={<AdminListPage />} />
            <Route path="*" element={<Navigate to="/" replace />} />
          </Routes>
        </Container>
      </Router>
    </UserProvider>
  );
}

export default App;