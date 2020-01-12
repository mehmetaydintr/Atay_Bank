/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */
import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import {createDrawerNavigator} from 'react-navigation-drawer';
import React from 'react';
import Login from './src/Login';
import Register from './src/Register';
import Anasayfa from './src/Anasayfa';
import Header from './src/Header';
import HesaplarListe from './src/HesaplarListe';
import Listele from './src/Listele';
import ParaYatır from './src/ParaYatır';
import ParaCek from './src/ParaCekme';
import Havale from './src/Havale';
import Transferİslemleri from './src/Transferİslemleri';
import Virman from './src/Virman';
import HGSListele from './src/HGSListele';
import HGSAc from './src/HGSAc';
import HGSParaYatir from './src/HGSParaYatir';
import KrediTahmini from './src/KrediTahmini.js';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  TextInput,
  StatusBar,
  Button,
  Icon,
} from 'react-native';

// const RootStack = createStackNavigator(
//   {
//     Home: Login,
//     Details: Register,
//     has: Anasayfa,
//   },
//   {
//     initialRouteName: 'Home',
//   },
// );

const MyDrawerNavigator = createDrawerNavigator(
  {
    Home: {
      screen: Login,
      drawerLabel: () => null,
    },
    Anasayfa: {
      screen: Anasayfa,
    },
    Details: {
      screen: Register,
      drawerLabel: () => null,
    },
    Hesaplar: {
      screen: Listele,
    },
    ParaCek: {
      screen: ParaCek,
    },
    ParaYatır: {
      screen: ParaYatır,
    },
    Transfer: {
      screen: Transferİslemleri,
    },
    Havale: {
      screen: Havale,
      drawerLabel: () => null,
    },
    Virman: {
      screen: Virman,
      drawerLabel: () => null,
    },
    HGS: {
      screen: HGSListele,
    },
    HGSParaYatir: {
      screen: HGSParaYatir,
      drawerLabel: () => null,
    },
    HGSAc: {
      screen: HGSAc,
      drawerLabel: () => null,
    },
    KrediTahmini: {
      screen: KrediTahmini,
    },
  },
  {
    initialRouteName: 'Home',
  },
);
const AppContainer = createAppContainer(MyDrawerNavigator);
export default class App extends React.Component {
  render() {
    return (
      <>
        <AppContainer />
      </>
    );
  }
}
