import React from 'react';
import { View } from 'react-native';

import RoundButton from '../RoundButton';
import RoundButton2 from '../RoundButton2';
import { COLORS } from '../utils/constants';
import { styles } from './styles';

export default function Footer({ handleChoice }) {
  return (
    <View style={styles.container}>
      <RoundButton
        name='../../../assets/x.png'
        size={34}
        color={'styles.container1'} 
        onPress={() => handleChoice(-1)}
      />
      <RoundButton2
        name='../../../assets/heart.png'
        size={34}
        color={'styles.container2'}
        onPress={() => handleChoice(1)}
      />
    </View>
  );
}
