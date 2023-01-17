import FormInput from './FormInput';
import { useState } from 'react';
import Card from 'react-bootstrap/Card';
import Button from 'react-bootstrap/Button';
import Swal from 'sweetalert2';


const apiUrlR= 'http://localhost:50874/api/Recipes';
const apiUrlI= 'http://localhost:50874/api/Ingredients';


export default function Recipe() {
  
  const [cards,setCards]= useState([])
  const [isButtonVisible, setButtonVisibility] = useState(false);

  const [recipeValues,setRecipeValues]= useState({
    name:"",
    cookingMethod:"",
    time:"",
    image:""
  });

  const inputs=[
    {
      id : 1,
      name:"name",
      type:"text",
      placeholder:"Enter name",
      label: "Name:"
    },
    {
      id : 2,
      name:"cookingMethod",
      type:"text",
      placeholder:"Insert Cooking method",
      label: "Cooking Method:"
    },
    {
      id : 3,
      name:"time",
      type:"text",
      placeholder:"Insert time",
      label: "Time:"
    },
    {
      id : 4,
      name:"image",
      type:"text",
      placeholder:"Enter image url",
      label: "Image:"
    }
  ]

  const onChange = (e)=>{
    setRecipeValues({...recipeValues,[e.target.name]: e.target.value})
  }

  const handleClick=(e)=>{
    e.preventDefault();

    fetch(apiUrlR, {
      method: 'POST',
      body: JSON.stringify(recipeValues),
      headers: new Headers({
      'Content-type': 'application/json; charset=UTF-8',
      'Accept':'application/json; charset=UTF-8'
      })
      })
      .then(res => {
      console.log('res=', res);
      return res.json()
      })
      .then(
      (result) => {
      console.log("fetch POST= ", result);
      },
      (error) => {
      console.log("err post=", error);
    });

    
  }

  const confirmIngredient=()=>{
    Swal.fire({
      position: 'top-end',
      icon: 'success',
      title: 'Your Ingredients was selected!',
      showConfirmButton: false,
      timer: 1500
    })
    const Tcards=[...cards]
    const cardsTmp=[];
    for(let i=0;i< Tcards.length;i++){
      if(Tcards[i].isChecked){cardsTmp.push(Tcards[i].ingr.IngredientsID)}
    }
    
    setRecipeValues({...recipeValues,Ingredient_data : cardsTmp})

  }
  



  const printAllIngredient=()=>{
    setButtonVisibility(true);

    fetch(apiUrlI, {
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
      
      console.log("fetch FetchGetIngredients= ", result);
      result.map(ingr => console.log(ingr.IngredientsID));
      
      const cardsTmp=[];
      result.map(ingr =>cardsTmp.push({ingr, isChecked : false}) );
      console.log(cardsTmp);
      setCards([...cardsTmp]);
      
    },
    (error) => {
      console.log("err post=", error);
    });

   

  }

  const reset=()=>{
    inputs.map((input) => (
      <FormInput  
        key={input.id}
        value={recipeValues[input.name]} 
        onChange={onChange}
      />
    ))
  }

  const handleChange = (event,index) => {
    
    console.log(index);

    const Tcards=[...cards]
    Tcards[index].isChecked= event.target.checked
    setCards([...Tcards]);  
    
  };

  return (
   <div >

    <form  style = {{margin: 80}} >

      {inputs.map((input) => (
        <FormInput  
          {...input} 
          key={input.id}
          value={recipeValues[input.name]} 
          onChange={onChange}
        />
      ))}

      <button onClick={handleClick}>Create new recipe</button>
      <button onClick={reset}>reset Form</button>
       
    </form>

    {isButtonVisible && <Button onClick={confirmIngredient} style={{ display: 'block', margin: 'auto' }} variant="primary" size="lg"> Press To CONFIRM Ingredients chosed </Button>}
    <br/>
    <button onClick={printAllIngredient}>Get All ingredients</button>
          
    {cards.map((card,index)=>
      {return <Card key={index}  style={{ width: '18rem', float: 'left' }}>
        <Card.Img variant="top" src={card.ingr.Image} /> 
          <Card.Body>
            <input 
              value={index}
              type="checkbox"
              checked={cards.isChecked}
              onChange={(event)=>handleChange(event,index)}
            />
            <Card.Title>Name: {card.ingr.Name} <br/> Calories: {card.ingr.Calories}</Card.Title>
          </Card.Body>
        </Card>
    })}
          

        

   </div>
  )
}
