import React from 'react';
import {Text, View, Button} from 'react-native';
import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import {createDrawerNavigator} from 'react-navigation-drawer';
import {DrawerItems} from 'react-navigation-drawer';
const Header = ({headerText}) => {
  const {textStyle, viewStyle} = styles;
  return (
    <>
      <View style={viewStyle}>
        <Text style={textStyle}> {headerText} </Text>
      </View>
    </>
  );
};

const styles = {
  textStyle: {
    fontSize: 20,
  },
  viewStyle: {
    backgroundColor: '#6495ed',
    height: 60,
    justifyContent: 'center',
    alignItems: 'center',
    paddingTop: 15,
    shadowOffset: {width: 0, height: 2},
    shadowOpacity: 0.2,
  },
};

export default Header;
