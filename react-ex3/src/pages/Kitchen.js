import { useState,useEffect } from 'react';
import Card from 'react-bootstrap/Card';

const apiUrlR= 'http://localhost:50874/api/Recipes';

export default function Kitchen() {
  const [Recipes,setRecipes]= useState([])

  useEffect(() => {
    fetch(apiUrlR, {
      method: 'GET',
      headers: new Headers({
      'Content-Type': 'application/json; charset=UTF-8',
      'Accept':'application/json; charset=UTF-8'
      })
    })
    .then(res => {
      console.log('res=', res);
      console.log('res.status', res.status);
      console.log('res.ok', res.ok);
      return res.json()
    })
    .then(
      (result) => { 
      console.log("fetch FetchGetRecipes= ", result);
      result.map(recipes => console.log(recipes));
      
      setRecipes(result);   
    },
    (error) => {
      console.log("err post=", error);
    });
  }, []);

  

  return (
    <div>
      {console.log(Recipes)}
      {Recipes.map((recipe,index)=>
      {return <Card key={index}  style={{ width: '18rem', float: 'left' }}>
        <Card.Img variant="top" src={recipe.Image} /> 
          <Card.Body>
            <Card.Title>Name:  {recipe.Name} <br/> CookingMethod:  {recipe.CookingMethod} <br/> Time:  {recipe.Time}</Card.Title>            
          </Card.Body>
        </Card>
      })}

    </div>
  )
  
}
