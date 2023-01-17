import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

import {Route, Routes } from 'react-router-dom';
import Kitchen from './pages/Kitchen';
import Ingredient from './pages/Ingredient';
import Recipe from './pages/Recipe';



function App() {
  

  return (
  <> 
        <Navbar bg="primary" variant="dark">
          <Container>
            <Navbar.Brand to="/">Navbar</Navbar.Brand>
            <Nav className="me-auto">
              <Nav.Link href = "/" >My Kitchen</Nav.Link>
              <Nav.Link href = "/ingredients" >Create new ingredient</Nav.Link>
              <Nav.Link href = "/recipes" >Create new recipe</Nav.Link>
            </Nav>
          </Container>
        </Navbar>

        <Routes>
          <Route path="/" element={<Kitchen/>}/>
          <Route path="/ingredients" element={<Ingredient/>}/>
          <Route path="/recipes" element={<Recipe/>}/>
        </Routes>
  </>
  
  );
}

export default App;
