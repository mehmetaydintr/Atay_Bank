import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import React from 'react';
import axios from 'axios';
import Header from './Header';
import Listele from './Listele';
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
  Alert,
  Platform,
} from 'react-native';
import {multipleValidOptions} from 'jest-validate/build/condition';
import CommonDataManager from './CommonDataManager';

export default class HesaplarListe extends React.Component {
  state = {data: '', musteriNo: ''};
  constructor(props) {
    super(props);
    this.state = {data: this.props.data};
  }
  hesapSil() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo2 = commonData.getMusteriNo();
    console.log('Hesap Sil');
    console.log(this.state.data.hesapNo);
    console.log(this.state.musteriNo);
    axios({
      method: 'put',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/account/hesapSil',
      data: {
        musteriNo: musteriNo2,
        hesapNo: this.state.data.hesapNo,
      },
    }).then(response => {
      console.log(response.status);
      this.setState({status: response.status});
      if (this.state.status == '200') {
        this.setState({data: response.data});
        console.log('APİ: ', this.state.data);
      } else {
        Alert.alert(response.data);
      }
    });
  }
  render() {
    const {TextStyle, ButtonStyle1} = styles;
    return (
      <View>
        <View style={TextStyle}>
          <Text>Hesap No: {this.state.data.hesapNo}</Text>
        </View>
        <View style={TextStyle}>
          <Text>Bakiye: {this.state.data.bakiye} ₺</Text>
        </View>
        <View style={ButtonStyle1}>
          <Button title="Sil" onPress={() => this.hesapSil()} />
        </View>
      </View>
    );
  }
}

const styles = StyleSheet.create({
  TextStyle: {
    borderBottomWidth: 2,
    borderBottomColor: '#6495ed',
    marginLeft: 5,
    marginTop: 10,
    marginBottom: 10,
    color: 'grey',
  },
  ButtonStyle1: {
    flexDirection: 'row',
    marginRight: 10,
    marginTop: 10,
    justifyContent: 'flex-end',
    position: 'relative',
    borderRadius: 50,
  },
});
