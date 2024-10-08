import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import './Header.css'; // Importando o CSS personalizado

const Header = () => {
  return (
    <Navbar className="navbar-custom" expand="lg" fixed="top">
      <Navbar.Brand as={Link} to="/" className="ml-auto">Clínica Saúde</Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="ml-auto">
          <Nav.Link as={Link} to="/pacientes">Pacientes</Nav.Link>
          <Nav.Link as={Link} to="/atendimento">Atendimento</Nav.Link>
          <Nav.Link as={Link} to="/triagem">Triagem</Nav.Link>
          <Nav.Link as={Link} to="/especialidade">Especialidade</Nav.Link>
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  );
};

export default Header;
