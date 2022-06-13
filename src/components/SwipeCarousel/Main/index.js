import React, { useCallback, useEffect, useRef, useState } from 'react';
import { Animated, PanResponder, View } from 'react-native';
import axios from 'axios';
import Card from '../Card';
import Footer from '../Footer';
import { ACTION_OFFSET, CARD } from '../utils/constants';
import { usersName as usersArray, pets as petsArray } from './data';
import { styles } from './styles';

export default function Main() {

  //const [pets, setPets] = useState(petsArray);
  const [users, setUsers] = useState([]);
  const [nr, setNr] = useState(0);
  //setTimeout(() => {usersArray.then((value) => {setUsers(value);})}, 1000);
  console.log(users);
  const swipe = useRef(new Animated.ValueXY()).current;
  const tiltSign = useRef(new Animated.Value(1)).current;
  useEffect(() => {
    if (!users.length) {
      if (nr==0) {
      setTimeout(() => {usersArray.then((value) => {setUsers(value);})}, 1000);
      setNr(1);
      }
      else if (nr == 1) {
        document.getElementById('run').style.display = 'block';
      }
    }
  }, [users.length]);

  const removeTopCard = useCallback(() => {
    setUsers((prevState) => prevState.slice(1));
    swipe.setValue({ x: 0, y: 0 });
  }, [swipe]);

  const panResponder = PanResponder.create({
    onMoveShouldSetPanResponder: () => true,
    onPanResponderMove: (_, { dx, dy, y0 }) => {
      swipe.setValue({ x: dx, y: dy });
      tiltSign.setValue(y0 > CARD.HEIGHT / 2 ? 1 : -1);
    },
    onPanResponderRelease: (_, { dx, dy }) => {
      const direction = Math.sign(dx);
      const isActionActive = Math.abs(dx) > ACTION_OFFSET;
      if (isActionActive) {
        console.log(users[0].name + " action:");
        if (direction == -1)
          console.log("swipe left");
        else if (direction == 1) {
          console.log("swipe right");
          var friendId = users[0].id;
          var userId = localStorage.getItem("userId");
          console.log(userId, friendId);
          axios.post("https://findastudybuddymds.azurewebsites.net/api/UserConnections/AddFriend?UserId="+userId+"&FriendId="+friendId)
            .then(response => {
              console.log(response);
            })
            .catch(error => {
              console.log(error);
            })
        }
        Animated.timing(swipe, {
          duration: 200,
          toValue: {
            x: direction * CARD.OUT_OF_SCREEN,
            y: dy,
          },
          useNativeDriver: true,
        }).start(removeTopCard);
      } else {
        Animated.spring(swipe, {
          toValue: {
            x: 0,
            y: 0,
          },
          useNativeDriver: true,
          friction: 5,
        }).start();
      }
    },
  });

  const handleChoice = useCallback(
    (direction) => {
      if (direction == -1)
          console.log("swipe left");
        else if (direction == 1)
          console.log("swipe right");
      Animated.timing(swipe.x, {
        toValue: direction * CARD.OUT_OF_SCREEN,
        duration: 400,
        useNativeDriver: true,
      }).start(removeTopCard);
    },
    [removeTopCard, swipe.x]
  );

  return (
    <View style={styles.container}>
      {users
        .map(({ id, name, source, yearOfStudy, courses }, index) => {
          const isFirst = index === 0;
          const dragHandlers = isFirst ? panResponder.panHandlers : {};

          return (
            <Card
              key={name}
              name={name+"\nYear: "+yearOfStudy}
              source={source}
              courses={courses}
              isFirst={isFirst}
              swipe={swipe}
              tiltSign={tiltSign}
              {...dragHandlers}
            />
            
          );
          
        })
        .reverse()}
        <h2 id='run' className='run-matches' style={{display: "none"}}>
          We've run out of 
          <br></br>potential matches for you.
        </h2>

    </View>
  );
}
