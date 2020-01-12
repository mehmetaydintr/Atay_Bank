import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import React from 'react';
import Login from './Login';
import Register from './Register';
import {DrawerItems} from 'react-navigation-drawer';
import Header from './Header';
import {BackHandler} from 'react-native';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  TextInput,
  StatusBar,
  Button,
  Image,
  Alert,
} from 'react-native';

import CommonDataManager from './CommonDataManager';

import {
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';

export default class Anasayfa extends React.Component {
  render() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo = commonData.getMusteriNo();
    console.log('Anasayfa', musteriNo);
    const {ViewStyle, subViewstyle, TextStyle, buttonStyle, container} = styles;
    return (
      <>
        <SafeAreaView>
          <Header headerText="Anasayfa" />
          <View style={container}>
            <Image style={container} source={require('./assets/banka.jpg')} />
          </View>
          <View style={buttonStyle}>
            <Button
              title="Open Drawer"
              onPress={() => this.props.navigation.toggleDrawer()}
            />
          </View>
        </SafeAreaView>
      </>
    );
  }
}

const styles = StyleSheet.create({
  ViewStyle: {
    borderWidth: 1,
    borderRadius: 2,
    borderColor: '#ddd',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'center',
  },
  buttonStyle: {
    flexDirection: 'row',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'flex-end',
    position: 'relative',
  },
  subViewstyle: {
    borderWidth: 1,
    borderRadius: 20,
    borderColor: '#ddd',
    marginTop: 10,
    marginRight: 5,
    marginLeft: 5,
    padding: 5,
    justifyContent: 'flex-start',
    flexDirection: 'row',
    position: 'relative',
  },
  TextStyle: {
    color: 'black',
    fontSize: 20,
    flex: 2,
    paddingRight: 5,
    paddingLeft: 5,
  },
  container: {
    width: 150,
    height: 150,
    alignItems: 'center',
    justifyContent: 'center',
    marginLeft: 90,
    marginTop: 10,
    marginBottom: 20,
  },
});
