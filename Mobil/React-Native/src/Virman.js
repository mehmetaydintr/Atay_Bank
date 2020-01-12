import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import React from 'react';
import axios from 'axios';
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
  Alert,
} from 'react-native';
import CommonDataManager from './CommonDataManager';
class Havale extends React.Component {
  state = {
    data: [],
    musteriNo: '',
    aliciHesapNo: '',
    Bakiye: '',
    Tutar: '',
  };
  static navigationOptions = {
    drawerLabel: () => null,
  };
  componentWillMount() {
    this.VirmanHesaplar();
  }
  VirmanYap() {
    console.log('Alici', this.state.aliciHesapNo);
    let gondericihesapNo = this.props.navigation.getParam(
      'gondericihesapNo',
      0,
    );
    let commonData = CommonDataManager.getInstance();
    console.log('Gönderici', gondericihesapNo);
    console.log('Tutar', this.state.Tutar);
    if (this.state.aliciHesapNo.length === 0) {
      Alert.alert('Lütfen bir alıcı hesap numarası giriniz.');
    } else {
      if (this.state.Tutar.length === 0) {
        Alert.alert('Tutar kısmı boş geçilemez');
      } else {
        axios({
          method: 'post',
          url:
            'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/transfer/virman',
          data: {
            aliciHesapNo: this.state.aliciHesapNo,
            gondericiHesapNo: gondericihesapNo,
            tutar: this.state.Tutar,
          },
        }).then(response => {
          console.log(response.status);
          this.setState({status: response.status});
          if (this.state.status == '200') {
            console.log(response.data);
            this.setState({Bakiye: response.data});
            Alert.alert(
              'İşleminiz başarılı bakiyeniz: ',
              response.data.toString(),
            );
            this.setState({Tutar: ''});
          } else {
            Alert.alert(response.data);
          }
        });
      }
    }
  }
  VirmanHesaplar() {
    let commonData = CommonDataManager.getInstance();
    let musteriNo2 = commonData.getMusteriNo();
    let gondericihesapNo = this.props.navigation.getParam(
      'gondericihesapNo',
      0,
    );
    axios({
      method: 'post',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/transfer/virman/hesaplar',
      data: {
        musteriNo: musteriNo2,
        hesapNo: gondericihesapNo,
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
    let bakiye = this.props.navigation.getParam('bakiye', 0);
    let gondericihesapNo = this.props.navigation.getParam(
      'gondericihesapNo',
      0,
    );
    const {
      ViewStyle,
      subViewstyle,
      TextStyle,
      buttonStyle,
      buttonStyle1,
    } = styles;
    var accounts = [];
    console.log('Virman data', this.state.data.length);
    for (let i = 0; i < this.state.data.length; i++) {
      //accounts.push(<HesaplarListe key={i} data={this.state.data[i]} />);
      accounts.push(
        <View key={i} style={subViewstyle}>
          <View style={TextStyle}>
            <Text
              onPress={() => {
                this.setState({aliciHesapNo: this.state.data[i].hesapNo});
              }}>
              Hesap No: {this.state.data[i].hesapNo}
            </Text>
          </View>
        </View>,
      );
    }
    return (
      <>
        <Header headerText="Virman Ekranı" />
        <View style={ViewStyle}>
          <View style={buttonStyle1}>
            <Button
              title="Drawer"
              onPress={() => this.props.navigation.toggleDrawer()}
            />
          </View>
          <View style={ViewStyle}>{accounts}</View>
          <View style={subViewstyle}>
            <Text>Bakiye: {bakiye} ₺ </Text>
          </View>
          <View style={subViewstyle}>
            <Text>Gönderici Hesap Numarası: {gondericihesapNo}</Text>
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="Alıcı Hesap Numarası"
              style={TextStyle}
              value={this.state.aliciHesapNo}
              onChangeText={text => this.setState({aliciHesapNo: text})}
              maxLength={20}
              keyboardType={'numeric'}
            />
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="Tutar"
              style={TextStyle}
              value={this.state.Tutar}
              onChangeText={text => this.setState({Tutar: text})}
              maxLength={20}
              keyboardType={'numeric'}
            />
          </View>
          <View style={buttonStyle}>
            <Button
              style={buttonStyle}
              title="Gönder"
              onPress={() => this.VirmanYap()}
            />
          </View>
        </View>
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
    marginTop: 10,
    flexDirection: 'row',
    justifyContent: 'flex-end',
    marginBottom: 10,
    marginRight: 5,
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
  buttonStyle1: {
    marginTop: 10,
    flexDirection: 'row',
    justifyContent: 'center',
    marginBottom: 10,
  },
});
export default Havale;
