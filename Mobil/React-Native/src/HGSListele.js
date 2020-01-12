import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import {createDrawerNavigator} from 'react-navigation-drawer';
import React from 'react';
import axios from 'axios';
import HesaplarListe from './HesaplarListe';
import Header from './Header';
import ParaCekme from './ParaCekme';
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
  Icon,
  Alert,
} from 'react-native';
import Axios from 'axios';
import CommonDataManager from './CommonDataManager';

class HGSListele extends React.Component {
  state = {data: [], musteriNo: '', hesapNo: ''};

  componentWillMount() {
    console.log('girdi');
    this.HGSGetir();
  }

  // renderData() {
  //   return this.state.data.map((responseData, id) => {
  //     console.log('responseData: ', responseData.hesapNo);
  //     <Text key={id}> {responseData.hesapNo} </Text>;
  //   });
  // }
  HGSGetir() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo2 = commonData.getMusteriNo();
    axios({
      method: 'post',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/hgs/hgsler',
      data: {
        HGSMusteriNumarasi: musteriNo2,
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
  yeniHesapAc() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo2 = commonData.getMusteriNo();
    axios({
      method: 'post',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/account/hesapAc',
      data: {
        musteriNo: musteriNo2,
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
    this.HGSGetir();
  }
  render() {
    const {TextStyle, buttonStyle, HesapAcButtonStyle, ButtonStyle1} = styles;
    console.log('gelen data', this.state.data);
    var accounts = [];
    for (let i = 0; i < this.state.data.length; i++) {
      //accounts.push(<HesaplarListe key={i} data={this.state.data[i]} />);
      accounts.push(
        <View key={i}>
          <View style={TextStyle}>
            <Text>HGS No: {this.state.data[i].HGSNumarası}</Text>
          </View>
          <View style={TextStyle}>
            <Text>Bakiye: {this.state.data[i].hgsBakiyesi} ₺</Text>
          </View>
          <View style={ButtonStyle1}>
            <Button
              title="Para Yükle"
              onPress={() => {
                this.props.navigation.navigate('HGSParaYatir', {
                  HGSNumarası: this.state.data[i].HGSNumarası,
                  hgsBakiyesi: this.state.data[i].hgsBakiyesi,
                });
              }}
            />
          </View>
        </View>,
      );
    }
    return (
      <>
        <ScrollView>
          <View>
            <Header headerText="HGS Ekranı" />
            <View style={HesapAcButtonStyle}>
              <Button
                title="Yeni HGS Aç"
                onPress={() => this.props.navigation.navigate('HGSAc')}
              />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Drawer"
                onPress={() => this.props.navigation.toggleDrawer()}
              />
            </View>
            <View style={buttonStyle}>
              <Button title="Yenile" onPress={() => this.HGSGetir()} />
            </View>
            {accounts}
          </View>
        </ScrollView>
      </>
    );
  }
}
const styles = StyleSheet.create({
  TextStyle: {
    borderBottomWidth: 2,
    borderBottomColor: 'black',
    marginTop: 10,
    marginBottom: 10,
  },
  buttonStyle: {
    flexDirection: 'column',
    marginRight: 120,
    marginLeft: 120,
    marginTop: 10,
    justifyContent: 'center',
    backgroundColor: '#000',
  },
  HesapAcButtonStyle: {
    flexDirection: 'column',
    marginRight: 120,
    marginLeft: 120,
    marginTop: 10,
    justifyContent: 'center',
    backgroundColor: '#000',
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

export default HGSListele;
