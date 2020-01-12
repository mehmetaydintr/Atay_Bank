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

class Listele extends React.Component {
  state = {data: [], musteriNo: '', hesapNo: ''};

  componentWillMount() {
    console.log('girdi');
    this.hesapGetir();
  }

  // renderData() {
  //   return this.state.data.map((responseData, id) => {
  //     console.log('responseData: ', responseData.hesapNo);
  //     <Text key={id}> {responseData.hesapNo} </Text>;
  //   });
  // }
  hesapGetir() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo2 = commonData.getMusteriNo();
    axios({
      method: 'post',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/account/hesaplar',
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
    this.hesapGetir();
  }
  hesapSil(props) {
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
        hesapNo: props,
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
    this.hesapGetir();
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
            <Text>Hesap No: {this.state.data[i].hesapNo}</Text>
          </View>
          <View style={TextStyle}>
            <Text>Bakiye: {this.state.data[i].bakiye} ₺</Text>
          </View>
          <View style={ButtonStyle1}>
            <Button
              title="Sil"
              onPress={() => {
                this.hesapSil(this.state.data[i].hesapNo);
              }}
            />
          </View>
          <View style={ButtonStyle1}>
            <Button
              title="Para Çek"
              onPress={() => {
                this.props.navigation.navigate('ParaCek', {
                  hesapNo: this.state.data[i].hesapNo,
                  bakiye: this.state.data[i].bakiye,
                });
              }}
            />
          </View>
          <View style={ButtonStyle1}>
            <Button
              title="Para Yatır"
              onPress={() => {
                this.props.navigation.navigate('ParaYatır', {
                  hesapNo: this.state.data[i].hesapNo,
                  bakiye: this.state.data[i].bakiye,
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
            <Header headerText="Hesaplar Ekranı" />
            <View style={HesapAcButtonStyle}>
              <Button
                title="Yeni Hesap Aç"
                onPress={() => this.yeniHesapAc()}
              />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Drawer"
                onPress={() => this.props.navigation.toggleDrawer()}
              />
            </View>
            <View style={buttonStyle}>
              <Button title="Yenile" onPress={() => this.hesapGetir()} />
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

export default Listele;
