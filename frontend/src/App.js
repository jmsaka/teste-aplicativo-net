import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Header';
import Paciente from './pages/Paciente';
import Atendimento from './pages/Atendimento';
import Triagem from './pages/Triagem';
import Especialidade from './pages/Especialidade';

function App() {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/" element={<Atendimento />} />
        <Route path="/pacientes" element={<Paciente />} />
        <Route path="/atendimento" element={<Atendimento />} />
        <Route path="/triagem" element={<Triagem />} />
        <Route path="/especialidade" element={<Especialidade />} />
      </Routes>
    </Router>
  );
}

export default App;
