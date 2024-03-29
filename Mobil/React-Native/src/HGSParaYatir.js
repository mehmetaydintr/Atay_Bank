import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import Header from './Header';
import {
  HeaderButtons,
  HeaderButton,
  Item,
} from 'react-navigation-header-buttons';
import React from 'react';
import axios from 'axios';
import CommonDataManager from './CommonDataManager';
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
  Image,
} from 'react-native';

import {
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';
class HGSParaYatir extends React.Component {
  state = {musteriNo: '', HGSNo: '', Bakiye: '', Tutar: ''};
  static navigationOptions = {
    drawerLabel: () => null,
  };
  componentWillMount() {
    let HGSNumarası = this.props.navigation.getParam('HGSNumarası', 0);
    let hgsBakiyesi = this.props.navigation.getParam('hgsBakiyesi', 0);
    this.setState({HGSNo: HGSNumarası});
    this.setState({Bakiye: hgsBakiyesi});
    console.log(this.state.HesapNo);
    console.log(this.state.Bakiye);
  }
  ParaYatır() {
    let HGSNumarası = this.props.navigation.getParam('HGSNumarası', 0);
    axios({
      method: 'put',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/hgs/paraYukle',
      data: {
        HGSNumarası: HGSNumarası,
        hgsBakiyesi: this.state.Tutar,
      },
    }).then(response => {
      console.log(response.status);
      this.setState({status: response.status});
      if (this.state.status == '200') {
        console.log(response.data);
        this.setState({Bakiye: response.data});
        Alert.alert(response.data.toString());
        this.setState({Tutar: ''});
        this.forceUpdate();
      } else {
        Alert.alert(response.data);
      }
    });
  }
  ParaYatırKurum() {
    let HGSNumarası = this.props.navigation.getParam('HGSNumarası', 0);
    console.log('hgs', HGSNumarası);
    console.log('tutar', this.state.Tutar);
    axios({
      method: 'put',
      url:
        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/hgskurum/paraYukle',
      data: {
        HGSNO: HGSNumarası,
        hgsBakiye: this.state.Tutar,
      },
    }).then(response => {
      console.log(response.status);
      this.setState({status: response.status});
      if (this.state.status == '200') {
        console.log(response.data);
        this.ParaYatır();
      } else {
        Alert.alert(response.data);
      }
    });
  }
  render() {
    let bakiye = this.props.navigation.getParam('hgsBakiyesi', 0);
    let HGSNumarası = this.props.navigation.getParam('HGSNumarası', 0);
    const {
      ViewStyle,
      subViewstyle,
      TextStyle,
      buttonStyle,
      container,
      InputStyle,
      TextInputStyle,
    } = styles;
    return (
      <>
        <SafeAreaView>
          <Header headerText="HGS Bakiye Yükle" />
          <View>
            <View style={TextStyle}>
              <Text>HGS Numarası:{HGSNumarası}</Text>
            </View>
            <View style={TextStyle}>
              <Text>Bakiye:{bakiye} ₺ </Text>
            </View>
            <View style={InputStyle}>
              <TextInput
                placeholder="Tutar"
                style={TextInputStyle}
                value={this.state.Tutar}
                onChangeText={text => this.setState({Tutar: text})}
                maxLength={18}
                keyboardType={'numeric'}
              />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Para Yatır"
                onPress={() => this.ParaYatırKurum()}
              />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Drawer"
                onPress={() => this.props.navigation.toggleDrawer()}
              />
            </View>
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
    marginTop: 20,
    color: 'black',
    fontSize: 30,
    flex: 2,
    paddingRight: 5,
    paddingLeft: 5,
    marginBottom: 20,
    marginLeft: 10,
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
  InputStyle: {
    borderBottomWidth: 1,
    marginRight: 5,
    marginLeft: 5,
    justifyContent: 'flex-start',
    flexDirection: 'row',
    position: 'relative',
  },
  TextInputStyle: {
    marginTop: 20,
    color: 'black',
    fontSize: 15,
    flex: 2,
    paddingRight: 5,
    paddingLeft: 5,
  },
});
export default HGSParaYatir;
